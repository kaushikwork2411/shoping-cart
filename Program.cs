using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace OnlineShopping;

//public class ErrorHandle : Exception
//{
//    public ErrorHandle(string msg) : base(msg)
//    {

//    }
//}

public static class checkFunctionHold
{
    public static void CartDoesNotHaveItem(string productName)
    {
        throw new Exception($"you doesn't add {productName}");
    }

    public static void outOfStockItem(string productName)
    {
        throw new Exception($"{productName} is out of stock...");
    }
}
class User
{
    //variables

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }    
    public string Address { get; set; }

    
    public ShoppingCart ShoppingCart { get; set; }
  

    //constructor

    public User(int id,string name,string email,string password,string address)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Address = address;
        ShoppingCart = new ShoppingCart();
        
    }


    public void displayUserData()
    {
        Console.WriteLine($"{Id} {Name} {Email} {Password} {Address}");
    }

    public void addTocart(Product product)
    {
        ShoppingCart.AddItem(product);
    }

    public void removeTocart(Product product)
    {
        ShoppingCart.RemoveItem(product);
    }

    public void Checkout()
    {
        Order order = new Order(this, ShoppingCart.Items);
        order.GenrateInvoice();
        order.ShipOrder();
        ShoppingCart.ClearItem();
    }
}

class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Price { get; set; }
    public int StockQuantitiy { get; set; }
    public string Brand { get; set; }
    public int totalStockQuantity { get; set; }

    public Product(int id, string name,string brand, string discription, int price, int quantitiy)
    {
        ProductId = id;
        ProductName = name;
        ProductDescription = discription;
        Price = price;
        StockQuantitiy = quantitiy;
        Brand = brand;
        totalStockQuantity = quantitiy;
    }


    public void UpdateStock(int quantity)
    {
        StockQuantitiy-=quantity;
    }

    public void DisplayProductDetails()
    {
        Console.WriteLine($"Product: {ProductName},Brand : {Brand}, Price: {Price}, Stock: {StockQuantitiy} \n");
    }

    public void  AvalibleProductStock()
    {
        Console.WriteLine($"Brand : {Brand}, Stock: {StockQuantitiy} is avalible out of {totalStockQuantity}");
    }
}

class ShoppingCart
{
    public int cartId { get; set;}
    public List<Product> Items { get; set; }
    public int TotalAmount {  get; set; }

    public ShoppingCart()
    {
        cartId = GetHashCode();
        Items = new List<Product>();
        TotalAmount = 0;
    }
    public void AddItem(Product product)
    {
        if (product.StockQuantitiy != 0)
        {
            Items.Add(product);
            TotalAmount += product.Price;
            product.UpdateStock(1);
        }
        else
        {
            checkFunctionHold.outOfStockItem(product.ProductName);
        }
       
    }

    public void RemoveItem(Product product)
    {
        if (Items.Contains(product))
        {
            Items.Remove(product);
            TotalAmount -= product.Price ;
            product.UpdateStock(-1);
        }
        else
        {
            checkFunctionHold.CartDoesNotHaveItem( product.ProductName);
        }
    }
    public void CalculateTotal()
    {
        Console.WriteLine($"Total Amount: {TotalAmount}");
    }

    public void ClearItem()
    {
        Items.Clear();
        TotalAmount= 0;
    }
}

class Order
{
    public int OrderId { get; set; }
    public User user { get; set; }
    public List<Product> item { get; set; }

    public DateTime OrderDate { get; set; }
    public decimal TotalAmount {get; set;}


    public Order(User user, List<Product> items)
    {
        this.OrderId = GetHashCode(); 
        this.user = user;
        this.item = items;
        this.OrderDate = DateTime.Now;
        this.TotalAmount = items.Sum(item => item.Price);
    }


    public void GenrateInvoice()
    {
        Console.WriteLine($"\norder id is : {OrderId}");
        Console.WriteLine($"order date : {OrderDate}");
        Console.WriteLine("Items:");
        foreach (var item in item)
        {
            Console.WriteLine($"  {item.ProductName} - {item.Price}");
        }
        Console.WriteLine($"Total Amount: {TotalAmount}\n");
    }

    public void ShipOrder()
    {
        Console.WriteLine($"order id is : {OrderId} and shipping Address is {user.Address}");
    }

}
class Program
{
    static void Main(string[] args)
    {
        //laptop brand 
        Product Apple = new Product(1, "Laptop","Apple", "High-performance laptop", 1200, 10);
        Product Dell = new Product(1, "Laptop", "Dell", "High-performance laptop", 18800, 10);
        Product Hp = new Product(1, "Laptop","Hp" ,"High-performance laptop", 200, 10);
        Product Lenevo = new Product(1, "Laptop","Lenevo" ,"High-performance laptop", 1890, 1);
        Product Samsung = new Product(1, "Laptop", "Samsung" ,"High-performance laptop", 750, 10);
        Product Asus = new Product(1, "Laptop","asus" ,"High-performance laptop", 120, 10);
        Product Lg = new Product(1, "Laptop","Lg" ,"High-performance laptop", 140, 10);


        //phone brand
        Product Iphone = new Product(2, "Smartphone", "Iphone", "Latest smartphone model", 800, 20);
        Product Ssamsung = new Product(2, "Smartphone", "samsung", "Latest smartphone model", 800,0);
        Product Realme = new Product(2, "Smartphone", "realme", "Latest smartphone model", 800, 20);
        Product Xiamome = new Product(2, "Smartphone", "xiamome", "Latest smartphone model", 800, 20);
        Product Vivo = new Product(2, "Smartphone", "vivo", "Latest smartphone model", 800, 20);
        Product oneplus = new Product(2, "Smartphone", "onePlus", "Latest smartphone model", 800, 20);

        //books
        Product Harrypoter = new Product(3, "books","Harrypoter", "top 1", 80, 0);
        Product richdad = new Product(3, "books", "rich dad poor dad", "top 1", 80, 80);
        Product friends = new Product(3, "books", "how to win friends", "top 1", 80, 80);
        Product elonMusk = new Product(3, "books", "elonmusk", "top 1", 80, 80);

        //electrnics tv
        Product panasonic = new Product(3, "TV", "panasonic", "high quality display", 780, 0);
        Product sony = new Product(3, "TV", "sony", "high quality display", 780, 689);
        Product samsungtv = new Product(3, "TV", "samsungtv", "high quality display", 780, 689);
        Product lgtv = new Product(3, "TV", "lgtv", "high quality display", 780, 689);

        //foods
        Product kajukatli = new Product(3, "kajukatli", "kajukatli","No 1 brand's bread", 2589, 0);
        Product oats = new Product(3, "oats", "oats", "No 1 brand's bread", 2589, 1547);
        Product dogfood = new Product(3, "dogfood", "dogfood", "No 1 brand's bread", 2589, 1547);
        Product chocolate = new Product(3, "chocolate", "chocolate", "No 1 brand's bread", 2589, 1547);

        //list of brand in category of items
        List<string> laptopBrands = new List<string> { "Lenovo","Dell", "HP" ,"Apple","Samsung","Asus","Lg"};
        List<string> mobileBrands = new List<string> { "Samsung", "Iphone", "OnePlus", "Xiaomi","Realme","Vivo" };
        List<string> books = new List<string> { "Harrypoter", "richdad", "friends", "elonMusk"};
        List<string> tv = new List<string> { "panasonic", "sony", "samsungtv", "lgtv" };
        List<string> foods = new List<string> { "kajukatli", "oats", "dogfood", "chocolate" };


        User user = new User(1001, "kaushik jagani", "kaushikjagani26@gmail.com", "password123", "123 tulsi arcade");

        bool exitloop = false;

        while (!exitloop)
        {
            Console.WriteLine($"\n1 for add product in cart.\n2 for remove product from cart\n3 for display items in cart" +
                $"\n4 for checkout\n5 for display avalible quentity of product.\n6 for exit ");
            int casehandle = Convert.ToInt32(Console.ReadLine());
            switch (casehandle)
            {
                case 1:
                    Console.WriteLine("Choose a product category:");
                    Console.WriteLine("1. Laptop");
                    Console.WriteLine("2. Mobile Phone");
                    Console.WriteLine("3.Books");
                    Console.WriteLine("4. Tv");
                    Console.WriteLine("5. Food");

                    int categoryChoice= Convert.ToInt32(Console.ReadLine());
                    
                    switch (categoryChoice)
                    {
                        case 1:
                            Console.WriteLine("Choose a laptop brand:");
                            for (int i = 0; i < laptopBrands.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {laptopBrands[i]}");
                            }
                            int brandChoice= Convert.ToInt32(Console.ReadLine());
                            string selectedBrand = laptopBrands[brandChoice - 1];
                            Product? selectedLaptop = null;

                            switch (selectedBrand)
                            {
                                case "Lenovo":
                                    selectedLaptop = Lenevo;
                                    break;
                                case "Dell":
                                    selectedLaptop = Dell;
                                    break;
                                case "HP":
                                    selectedLaptop = Hp;
                                    break;
                                case "Apple":
                                    selectedLaptop = Apple;
                                    break;
                                case "Samsung":
                                    selectedLaptop = Samsung;
                                    break;
                                case "Asus":
                                    selectedLaptop = Asus;
                                    break;
                                case "Lg":
                                    selectedLaptop = Lg;
                                    break;
                            }
                            if (selectedLaptop != null)
                            {
                                try
                                {
                                    user.addTocart(selectedLaptop);
                                    Console.WriteLine($"{selectedLaptop.Brand} {selectedLaptop.ProductName} added to the cart.");
                                }catch(Exception msg)
                                {
                                    Console.WriteLine(msg.Message);
                                }
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;

                        case 2:
                            Console.WriteLine("Choose a mobile Brands :");
                            for (int i = 0; i < mobileBrands.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {mobileBrands[i]}");
                            }
                            int MobilebrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedMobileBrand = mobileBrands[MobilebrandChoice - 1];
                            Product? selectedMobile = null;

                            switch (selectedMobileBrand)
                            {
                                case "Samsung":
                                    selectedMobile = Ssamsung;
                                    break;
                                case "Iphone":
                                    selectedMobile = Iphone;
                                    break;
                                case "OnePlus":
                                    selectedMobile = oneplus;
                                    break;
                                case "Xiaomi":
                                    selectedMobile = Xiamome;
                                    break;
                                case "Realme":
                                    selectedMobile = Realme;
                                    break;
                                case "Vivo":
                                    selectedMobile = Vivo;
                                    break;
                            }
                            if (selectedMobile != null)
                            {
                                try
                                {
                                    user.addTocart(selectedMobile);
                                    Console.WriteLine($"{selectedMobile.Brand} {selectedMobile.ProductName} added to the cart.");
                                }
                                catch (Exception msg)
                                {
                                    Console.WriteLine(msg.Message);
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("Invalid mobile brand.");
                            }
                            break;
                        case 3:
                            Console.WriteLine("Choose a Books Brands :");
                            for (int i = 0; i < books.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {books[i]}");
                            }
                            int bookbrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedBookBrand = books[bookbrandChoice - 1];
                            Product? selectedBook = null;

                            switch (selectedBookBrand)
                            {
                                case "Harrypoter":
                                    selectedBook = Harrypoter;
                                    break;
                                case "richdad":
                                    selectedBook = richdad;
                                    break;
                                case "friends":
                                    selectedBook = friends;
                                    break;
                                case "elonMusk":
                                    selectedBook = elonMusk;
                                    break;
                            }
                            if (selectedBook != null)
                            {
                                try
                                {
                                    user.addTocart(selectedBook);
                                    Console.WriteLine($"{selectedBook.Brand} {selectedBook.ProductName} added to the cart.");
                                }
                                catch (Exception msg)
                                {
                                    Console.WriteLine(msg.Message);
                                }
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid Book brand.");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Choose a TV Brands :");
                            for (int i = 0; i < tv.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {tv[i]}");
                            }
                            int tvbrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedTvBrand = tv[tvbrandChoice - 1];
                            Product? selectedTv = null;

                            switch (selectedTvBrand)
                            {
                                case "panasonic":
                                    selectedTv = panasonic;
                                    break;
                                case "sony":
                                    selectedTv = sony;
                                    break;
                                case "samsungtv":
                                    selectedTv = samsungtv;
                                    break;
                                case "lgtv":
                                    selectedTv = lgtv;
                                    break;
                            }
                            if (selectedTv != null)
                            {
                                try
                                {
                                    user.addTocart(selectedTv);
                                    Console.WriteLine($"{selectedTv.Brand} {selectedTv.ProductName} added to the cart.");
                                }
                                catch (Exception msg)
                                {
                                    Console.WriteLine(msg.Message);
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("Invalid Tv brand.");
                            }
                            break;
                        case 5:
                            Console.WriteLine("Choose a Food Brands :");
                            for (int i = 0; i < foods.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {foods[i]}");
                            }
                            int foodbrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedFoodBrand = foods[foodbrandChoice - 1];
                            Product? selectedFood = null;

                            switch (selectedFoodBrand)
                            {
                                case "kajukatli":
                                    selectedFood = kajukatli;
                                    break;
                                case "oats":
                                    selectedFood = oats;
                                    break;
                                case "dogfood":
                                    selectedFood = dogfood;
                                    break;
                                case "chocolate":
                                    selectedFood = chocolate;
                                    break;
                            }
                            if (selectedFood != null)
                            {
                                try
                                {
                                    user.addTocart(selectedFood);
                                    Console.WriteLine($"{selectedFood.Brand} {selectedFood.ProductName} added to the cart.");
                                }
                                catch (Exception msg)
                                {
                                    Console.WriteLine(msg.Message);
                                }
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid food brand.");
                            }
                            break;
                        default:
                            Console.WriteLine("you enter invalid input!!!");
                            break;
                    }

                    break;
                case 2:
                    Console.WriteLine("Product in cart");
                    foreach (var item in user.ShoppingCart.Items)
                    {
                        item.DisplayProductDetails();
                    }
                    Console.WriteLine("Choose a product category:");
                    Console.WriteLine("1. Laptop");
                    Console.WriteLine("2. Mobile Phone");
                    Console.WriteLine("3.Books");
                    Console.WriteLine("4. Tv");
                    Console.WriteLine("5. Food");

                    int categoryChoiceforRemove = Convert.ToInt32(Console.ReadLine());

                    switch (categoryChoiceforRemove)
                    {
                        case 1:
                            Console.WriteLine("Choose a laptop brand:");
                            for (int i = 0; i < laptopBrands.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {laptopBrands[i]}");
                            }
                            int brandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedBrand = laptopBrands[brandChoice - 1];
                            Product? selectedLaptop = null;

                            switch (selectedBrand)
                            {
                                case "Lenovo":
                                    selectedLaptop = Lenevo;
                                    break;
                                case "Dell":
                                    selectedLaptop = Dell;
                                    break;
                                case "HP":
                                    selectedLaptop = Hp;
                                    break;
                                case "Apple":
                                    selectedLaptop = Apple;
                                    break;
                                case "Samsung":
                                    selectedLaptop = Samsung;
                                    break;
                                case "Asus":
                                    selectedLaptop = Asus;
                                    break;
                                case "Lg":
                                    selectedLaptop = Lg;
                                    break;
                            }
                            if (selectedLaptop != null)
                            {
                                try
                                {
                                    user.removeTocart(selectedLaptop);
                                    Console.WriteLine($"{selectedLaptop.Brand} {selectedLaptop.ProductName} remove to the cart.");
                                }
                                catch (Exception msg)
                                {
                                    Console.WriteLine(msg.Message);
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;

                        case 2:
                            Console.WriteLine("Choose a mobile Brands :");
                            for (int i = 0; i < mobileBrands.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {mobileBrands[i]}");
                            }
                            int MobilebrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedMobileBrand = mobileBrands[MobilebrandChoice - 1];
                            Product? selectedMobile = null;

                            switch (selectedMobileBrand)
                            {
                                case "Samsung":
                                    selectedMobile = Ssamsung;
                                    break;
                                case "Iphone":
                                    selectedMobile = Iphone;
                                    break;
                                case "OnePlus":
                                    selectedMobile = oneplus;
                                    break;
                                case "Xiaomi":
                                    selectedMobile = Xiamome;
                                    break;
                                case "Realme":
                                    selectedMobile = Realme;
                                    break;
                                case "Vivo":
                                    selectedMobile = Vivo;
                                    break;
                            }
                            if (selectedMobile != null)
                            {
                                try
                                {
                                    user.removeTocart(selectedMobile);
                                    Console.WriteLine($"{selectedMobile.Brand} {selectedMobile.ProductName} remove to the cart.");
                                }
                                catch (Exception msg)
                                {
                                    Console.WriteLine(msg.Message);
                                }
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;
                        case 3:
                            Console.WriteLine("Choose a Books Brands :");
                            for (int i = 0; i < books.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {books[i]}");
                            }
                            int bookbrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedBookBrand = books[bookbrandChoice - 1];
                            Product? selectedBook = null;

                            switch (selectedBookBrand)
                            {
                                case "Harrypoter":
                                    selectedBook = Harrypoter;
                                    break;
                                case "richdad":
                                    selectedBook = richdad;
                                    break;
                                case "friends":
                                    selectedBook = friends;
                                    break;
                                case "elonMusk":
                                    selectedBook = elonMusk;
                                    break;
                            }
                            if (selectedBook != null)
                            {
                                try
                                {
                                    user.removeTocart(selectedBook);
                                    Console.WriteLine($"{selectedBook.Brand} {selectedBook.ProductName} remove to the cart.");
                                }
                                catch (Exception msg)
                                {
                                    Console.WriteLine(msg.Message);
                                }
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Choose a TV Brands :");
                            for (int i = 0; i < tv.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {tv[i]}");
                            }
                            int tvbrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedTvBrand = tv[tvbrandChoice - 1];
                            Product? selectedTv = null;

                            switch (selectedTvBrand)
                            {
                                case "panasonic":
                                    selectedTv = panasonic;
                                    break;
                                case "sony":
                                    selectedTv = sony;
                                    break;
                                case "samsungtv":
                                    selectedTv = samsungtv;
                                    break;
                                case "lgtv":
                                    selectedTv = lgtv;
                                    break;
                            }
                            if (selectedTv != null)
                            {
                                try
                                {
                                    user.removeTocart(selectedTv);
                                    Console.WriteLine($"{selectedTv.Brand} {selectedTv.ProductName} remove to the cart.");
                                }
                                catch (Exception msg)
                                {
                                    Console.WriteLine(msg.Message);
                                }
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;
                        case 5:
                            Console.WriteLine("Choose a Food Brands :");
                            for (int i = 0; i < foods.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {foods[i]}");
                            }
                            int foodbrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedFoodBrand = foods[foodbrandChoice - 1];
                            Product? selectedFood = null;

                            switch (selectedFoodBrand)
                            {
                                case "kajukatli":
                                    selectedFood = kajukatli;
                                    break;
                                case "oats":
                                    selectedFood = oats;
                                    break;
                                case "dogfood":
                                    selectedFood = dogfood;
                                    break;
                                case "chocolate":
                                    selectedFood = chocolate;
                                    break;
                            }
                            if (selectedFood != null)
                            {
                                try
                                {
                                    user.removeTocart(selectedFood);
                                    Console.WriteLine($"{selectedFood.Brand} {selectedFood.ProductName} remove to the cart.");
                                }
                                catch (Exception msg)
                                {
                                    Console.WriteLine(msg.Message);
                                }
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;
                        default:
                            Console.WriteLine("you enter invalid input!!!");
                            break;
                    }

                    break;
                case 3:
                    Console.WriteLine("Items in cart....");
                    foreach (var item in user.ShoppingCart.Items)
                    {
                        item.DisplayProductDetails();
                    }

                    break;
                case 4:
                    user.Checkout();

                    break;
                case 5: //show avalible stock of product
                    Console.WriteLine("Product in cart");
                    foreach (var item in user.ShoppingCart.Items)
                    {
                        item.DisplayProductDetails();
                    }
                    Console.WriteLine("Choose a product category:");
                    Console.WriteLine("1. Laptop");
                    Console.WriteLine("2. Mobile Phone");
                    Console.WriteLine("3.Books");
                    Console.WriteLine("4. Tv");
                    Console.WriteLine("5. Food");

                    int categoryChoiceforRemaining = Convert.ToInt32(Console.ReadLine());

                    switch (categoryChoiceforRemaining)
                    {
                        case 1:
                            Console.WriteLine("Choose a laptop brand:");
                            for (int i = 0; i < laptopBrands.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {laptopBrands[i]}");
                            }
                            int brandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedBrand = laptopBrands[brandChoice - 1];
                            Product? selectedLaptop = null;

                            switch (selectedBrand)
                            {
                                case "Lenovo":
                                    selectedLaptop = Lenevo;
                                    break;
                                case "Dell":
                                    selectedLaptop = Dell;
                                    break;
                                case "HP":
                                    selectedLaptop = Hp;
                                    break;
                                case "Apple":
                                    selectedLaptop = Apple;
                                    break;
                                case "Samsung":
                                    selectedLaptop = Samsung;
                                    break;
                                case "Asus":
                                    selectedLaptop = Asus;
                                    break;
                                case "Lg":
                                    selectedLaptop = Lg;
                                    break;
                            }
                            if (selectedLaptop != null)
                            {
                                selectedLaptop.AvalibleProductStock();
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;

                        case 2:
                            Console.WriteLine("Choose a mobile Brands :");
                            for (int i = 0; i < mobileBrands.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {mobileBrands[i]}");
                            }
                            int MobilebrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedMobileBrand = mobileBrands[MobilebrandChoice - 1];
                            Product? selectedMobile = null;

                            switch (selectedMobileBrand)
                            {
                                case "Samsung":
                                    selectedMobile = Ssamsung;
                                    break;
                                case "Iphone":
                                    selectedMobile = Iphone;
                                    break;
                                case "OnePlus":
                                    selectedMobile = oneplus;
                                    break;
                                case "Xiaomi":
                                    selectedMobile = Xiamome;
                                    break;
                                case "Realme":
                                    selectedMobile = Realme;
                                    break;
                                case "Vivo":
                                    selectedMobile = Vivo;
                                    break;
                            }
                            if (selectedMobile != null)
                            {
                                selectedMobile.AvalibleProductStock();
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;
                        case 3:
                            Console.WriteLine("Choose a Books Brands :");
                            for (int i = 0; i < books.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {books[i]}");
                            }
                            int bookbrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedBookBrand = books[bookbrandChoice - 1];
                            Product? selectedBook = null;

                            switch (selectedBookBrand)
                            {
                                case "Harrypoter":
                                    selectedBook = Harrypoter;
                                    break;
                                case "richdad":
                                    selectedBook = richdad;
                                    break;
                                case "friends":
                                    selectedBook = friends;
                                    break;
                                case "elonMusk":
                                    selectedBook = elonMusk;
                                    break;
                            }
                            if (selectedBook != null)
                            {
                                selectedBook.AvalibleProductStock();
                                
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Choose a TV Brands :");
                            for (int i = 0; i < tv.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {tv[i]}");
                            }
                            int tvbrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedTvBrand = tv[tvbrandChoice - 1];
                            Product? selectedTv = null;

                            switch (selectedTvBrand)
                            {
                                case "panasonic":
                                    selectedTv = panasonic;
                                    break;
                                case "sony":
                                    selectedTv = sony;
                                    break;
                                case "samsungtv":
                                    selectedTv = samsungtv;
                                    break;
                                case "lgtv":
                                    selectedTv = lgtv;
                                    break;
                            }
                            if (selectedTv != null)
                            {
                                selectedTv.AvalibleProductStock();
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;
                        case 5:
                            Console.WriteLine("Choose a Food Brands :");
                            for (int i = 0; i < foods.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {foods[i]}");
                            }
                            int foodbrandChoice = Convert.ToInt32(Console.ReadLine());
                            string selectedFoodBrand = foods[foodbrandChoice - 1];
                            Product? selectedFood = null;

                            switch (selectedFoodBrand)
                            {
                                case "kajukatli":
                                    selectedFood = kajukatli;
                                    break;
                                case "oats":
                                    selectedFood = oats;
                                    break;
                                case "dogfood":
                                    selectedFood = dogfood;
                                    break;
                                case "chocolate":
                                    selectedFood = chocolate;
                                    break;
                            }
                            if (selectedFood != null)
                            {
                                selectedFood.AvalibleProductStock();
                                
                            }
                            else
                            {
                                Console.WriteLine("Invalid laptop brand.");
                            }
                            break;
                        default:
                            Console.WriteLine("you enter invalid input!!!");
                            break;
                    }
                    break;
                case 6:
                    exitloop = true;
                    break;
                default:
                    Console.WriteLine("invalid input...");
                    break;
            }

        }
    }
}