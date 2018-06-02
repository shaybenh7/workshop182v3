using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.DAL;
using WebServices.Domain;

namespace wsep182.Domain
{
    public class ProductManager
    {
        private ProductDB productDB;
        private ProductInStoreDB productInStoreDB;
        private static ProductManager instance;
        private LinkedList<Product> products;
        private LinkedList<ProductInStore> productsInStores;
        private static int productInStoreId = 0;
        private static int productId = 0;
        private ProductManager()
        {
            productDB = new ProductDB(configuration.DB_MODE);
            productInStoreDB = new ProductInStoreDB(configuration.DB_MODE);
            products = productDB.Get();
            productsInStores = productInStoreDB.Get(); 
            productInStoreId = currProductInStoreIndex();
            productId = currProductIndex();
        }
        public static ProductManager getInstance()
        {   
            if (instance == null)
                instance = new ProductManager();
            return instance;
        }

        public static void restartInstance()
        {
            instance = new ProductManager();
        }
        public int currProductIndex()
        {
            LinkedList<Product> temp = productDB.Get();
            int index = 0;
            foreach (Product p in temp)
            {
                if (p.getProductId() > index)
                {
                    index = p.getProductId();
                }
            }
            return index;
        }
        public int currProductInStoreIndex()
        {
            LinkedList<ProductInStore> temp = productInStoreDB.Get();
            int index = 0;
            foreach (ProductInStore p in temp)
            {
                if (p.getProductInStoreId() > index)
                {
                    index = p.getProductInStoreId();
                }
            }
            return index;
        }
        public int getNextProductId()
        {
            productId = currProductIndex();
            return ++productId;
        }
        public int getNextProductInStoreId()
        {
            productInStoreId = currProductInStoreIndex();
            return ++productInStoreId;
        }
        public Product addProduct(String productName)
        {
            foreach (Product p in products)
                if (p.getProductName() == productName)
                    return null;
            Product newProduct = new Product(productName, getNextProductId());
            productDB.Add(newProduct);
            products.AddLast(newProduct);
            return newProduct;
        }

        public Boolean updateProduct(Product newProduct)
        {
            if (newProduct == null)
                return false;
            foreach (Product p in products)
                if (p.getProductId() == newProduct.getProductId())
                {
                    productDB.Remove(p);
                    products.Remove(p);
                    productDB.Add(newProduct);
                    products.AddLast(newProduct);
                    return true;
                }
            return false;
        }

        public Product getProduct(int productId)
        {
            foreach (Product p in products)
                if (p.getProductId() == productId)
                    return p;
            return null;
        }

        public LinkedList<Product> getAllProducts()
        {
            return products;
        }

        public Product getProductByName(String name)
        {
            foreach (Product p in products)
                if (p.getProductName() == name)
                    return p;
            return null;
        }
        public Boolean removeProduct(int productId)
        {
            foreach (Product p in products)
                if (p.getProductId() == productId)
                {
                    productDB.Remove(p);
                    products.Remove(p);
                    return true;
                }
            return false;
        }

        /*
         *         Product product;
        Store store;
        int quantity; //will be removed in the future
        double price;
        int isActive;
        int productInStoreId;
         */


        public ProductInStore addProductInStore(Product product, Store store, int quantity, double price)
        {
            ProductInStore newProduct;
            lock (this)
            {
                newProduct = new ProductInStore(getNextProductInStoreId(),"", product, price, quantity, store);
            }
            
            foreach (ProductInStore p in productsInStores)
                if (p.getProduct().getProductId() == newProduct.getProduct().getProductId() && p.getStore().getStoreId() == newProduct.getStore().getStoreId())
                    return null;
            productInStoreDB.Add(newProduct);
            productsInStores.AddLast(newProduct);
            return newProduct;
        }
        public ProductInStore addProductInStore(Product product, Store store, int quantity, double price,string category)
        {
            ProductInStore newProduct;
            lock (this)
            {
                newProduct = new ProductInStore(getNextProductInStoreId(), category, product, price, quantity, store);
            }
            if(productsInStores.Count>0)
                foreach (ProductInStore p in productsInStores)
                    if (p.getProduct().getProductId() == newProduct.getProduct().getProductId() && p.getStore().getStoreId() == newProduct.getStore().getStoreId())
                        return null;
            productInStoreDB.Add(newProduct);
            productsInStores.AddLast(newProduct);
            return newProduct;
        }


        public Boolean updateProductInStore(ProductInStore newProduct)
        {
            if (newProduct == null)
            {
                return false;
            }
            if (newProduct.getProduct() == null || newProduct.getStore() == null || newProduct.getAmount() < 0 || newProduct.getPrice() < 0)
                return false;
            foreach (ProductInStore p in productsInStores)
                if (p.getProduct().getProductId() == newProduct.getProduct().getProductId() && p.getStore().getStoreId() == newProduct.getStore().getStoreId())
                {
                    productInStoreDB.Remove(p);
                    productsInStores.Remove(p);
                    productInStoreDB.Add(newProduct);
                    productsInStores.AddLast(newProduct);
                    return true;
                }
            return false;
        }


        public ProductInStore getProductInStore(int productInStoreId)
        {
            foreach (ProductInStore p in productsInStores)
                if (p.getProductInStoreId() == productInStoreId)
                    return p;
            return null;
        }

        public LinkedList<ProductInStore> getProductsInStore(int storeId)
        {
            LinkedList<ProductInStore> ret = new LinkedList<ProductInStore>();
            foreach (ProductInStore p in productsInStores)
                if (p.getStore().getStoreId() == storeId)
                    ret.AddLast(p);
            return ret;
        }


 /*       public ProductInStore getProductInStore(int productId, int storeId)
        {
            foreach (ProductInStore p in productsInStores)
                if (p.getProduct().getProductId() == productId && p.getStore().getStoreId() == storeId)
                    return p;
            return null;
        }
*/
        public LinkedList<ProductInStore> getAllProductsInStore(int storeId)
        {
            LinkedList<ProductInStore> res = new LinkedList<ProductInStore>();
            foreach (ProductInStore p in productsInStores)
                if (p.getStore().getStoreId() == storeId)
                    if (p.getIsActive() == 1)
                        res.AddLast(p);
            return res;
        }

        public Boolean removeProductInStore(int productId, int storeId)
        {
            foreach (ProductInStore p in productsInStores)
                if (p.getProductInStoreId() == productId && p.getStore().getStoreId() == storeId)
                {
                    if (p.getIsActive() == 0)
                        return false;
                    if (SalesManager.getInstance().getSalesByProductInStoreId(p.getProductInStoreId()).Count>0)
                        return false;
                    productInStoreDB.Remove(p);
                    p.IsActive = 0;
                    productInStoreDB.Add(p);
                    return true;
                }
            return false;
        }

        public int getProductInStoreQuantity(int producInStoretId)
        {
            foreach (ProductInStore p in productsInStores)
                if (p.getProductInStoreId() == producInStoretId)
                {
                    return (p.getIsActive() == 1) ? p.getAmount() : 0;
                }
            return 0;
        }


        public LinkedList<ProductInStore> getAllProductsInStores()
        {
            LinkedList<ProductInStore> res = new LinkedList<ProductInStore>();
            foreach (ProductInStore p in productsInStores)
                if (p.getIsActive() == 1)
                    res.AddLast(p);
            return res;
        }
        
        public LinkedList<ProductInStore> searchProducts(String searchString)
        {
            LinkedList<ProductInStore> res = new LinkedList<ProductInStore>();
			StringComparison comp = StringComparison.OrdinalIgnoreCase;
			foreach (ProductInStore p in productsInStores)
				if (p.getIsActive() == 1 && (p.getProduct().getProductName().IndexOf(searchString, comp) != -1 || p.Category.IndexOf(searchString, comp) != -1))
					res.AddLast(p);
			return res;
        }
    }
}
