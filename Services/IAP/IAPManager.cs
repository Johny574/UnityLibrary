using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : Singleton<IAPManager> 
{
    public StoreController m_StoreController;
    [SerializeField] ProductsDictionary _products = new();
    public Dictionary<ProductDefinition.ProductCategory, Dictionary<string, Product>> ProductCache = new();
    public Dictionary<string, ProductDefinition.ProductClass> ProductClasses;
    void Start() => InitializeIAP();
    async void InitializeIAP() {
        m_StoreController = UnityIAPServices.StoreController();
        await m_StoreController.Connect();

        m_StoreController.OnPurchasePending += OnPurchasePending;
        m_StoreController.OnProductsFetched += OnProductsFetched;
        m_StoreController.OnPurchasesFetched += OnPurchasesFetched;

        var initialProductsToFetch = _products.Values.SelectMany(x => x.Definitions).Select(x => new UnityEngine.Purchasing.ProductDefinition(x.ID, x.GoogleProductID, x.Type)).ToList();
        m_StoreController.FetchProducts(initialProductsToFetch);
        m_StoreController.FetchPurchases();
    }
    
    void OnDisable() {
        m_StoreController.OnPurchasePending -= OnPurchasePending;
        m_StoreController.OnProductsFetched -= OnProductsFetched;
        m_StoreController.OnPurchasesFetched -= OnPurchasesFetched;
    }


    void OnProductsFetched(List<Product> products) {
        ProductCache = new();
        ProductClasses = new();
        foreach (var category in _products.Keys) {
            ProductCache[category] = new Dictionary<string, Product>();
            foreach (var product in _products[category].Definitions) {
                ProductClasses.Add(product.ID, product.Class);
                // Find the matching product in the fetched list
                var productHandle = products.Find(p => p.definition.id == product.ID);
                if (productHandle != null)
                    ProductCache[category][product.GoogleProductID] = productHandle;
            }
        }
    }

    private void OnPurchasePending(Order order) => ProcessPurchase(order);

    public void Buy(Product product) => m_StoreController.PurchaseProduct(product);

    private void OnPurchasesFetched(Orders orders) {
        foreach (var confirmedOrder in orders.ConfirmedOrders)
            ProcessPurchase(confirmedOrder);
    }

    private void ProcessPurchase(Order order) {
        string productId = order.Info.PurchasedProductInfo[0].productId;

        switch(ProductClasses[productId]) {
            case ProductDefinition.ProductClass.Currency:
                
            break;   
        }
    }
    
}