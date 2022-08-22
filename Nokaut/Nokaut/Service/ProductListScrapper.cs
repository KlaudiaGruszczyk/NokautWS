using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Extensions.Options;
using Nokaut.Models;

namespace Nokaut.Service;

public class ProductListScrapper : IProductListScrapper
{
    private readonly IOptions<NokautConfig> _nokautConfig;

    public ProductListScrapper(IOptions<NokautConfig> nokautConfig)
    {
        _nokautConfig = nokautConfig;
    
    }


    public async Task<ICollection<ProductModel>> GetProducts(string queryPhrase,
        CancellationToken cancellationToken = default)
    {
        var document = await FetchPage(queryPhrase, cancellationToken);

        if (string.IsNullOrEmpty(document.Text))
            return ArraySegment<ProductModel>.Empty;

        return document.QuerySelectorAll(".product-box")
            .Select(ExtractProduct)
            .ToList();
    }

    private async Task<HtmlDocument> FetchPage(string queryPhrase, CancellationToken cancellationToken = default)
    {
        var url = $"{_nokautConfig.Value.BaseUrl}/produkt:{queryPhrase}.html";
        var web = new HtmlWeb();

        try
        {
            return await web.LoadFromWebAsync(url, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new HtmlDocument();
        }
    }

    private ProductModel ExtractProduct(HtmlNode node)
    {
        var name = node
            .QuerySelector(".name")
            .InnerText;

        var price = node
            .QuerySelector(".price > strong")
            .Descendants()
            .Where(x => x is HtmlTextNode)
            .Skip(1)
            .FirstOrDefault()?
            .InnerText?
            .Trim();

        var href = node
            .QuerySelector(".url-box")
            .GetAttributeValue("href", null);

        return new ProductModel(name, price, href);
    }

   
}


