using PractcingRelationshipEntity.Models;
using PractcingRelationshipEntity.Models.Entity;

namespace PractcingRelationshipEntity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //using (var baseDados = new DataConteXT())
                //{
                    Product();
                    //baseDados.SaveChanges()
                //}
            }
            catch
            (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static Customer AdcCustomer()
        {
           var customer = new Customer();
           Console.WriteLine("Digite seu nome: ");
           customer.Name = Console.ReadLine();

            customer.Address = AddAddress();

            return customer;
        }
        static Address AddAddress()
        {
            var address = new Address();
            Console.Write("Digite o nome da rua: ");
            address.Street = Console.ReadLine();
            Console.WriteLine("Digite o numero: ");
            address.Number = Console.ReadLine();
            Console.WriteLine("Digite sua cidade: ");
            address.City = Console.ReadLine();

            return address;
        }
        static Provider Provider()
        {
            var provider = new Provider();

            Console.WriteLine("Nome Fornecedor: ");
            provider.Name = Console.ReadLine();

            Console.WriteLine("Digite o CNPJ: ");
            provider.CNPJ = Console.ReadLine();

            provider.Products = new List<Product> { Product() };

            return provider;
        }
        static Product Product()
        {
            using (var baseDados = new DataConteXT())
            {
                Provider? dado = baseDados.Providers.FirstOrDefault(x => x.Id == 1);

                var product = new Product();
                Console.WriteLine("Nome produto: ");
                product.Name = Console.ReadLine();

                Console.WriteLine("Valor: ");
                product.Value = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Descricao: ");
                product.Description = Console.ReadLine();


                dado.Products = new List<Product> { product };

                baseDados.SaveChanges();

                return product;
            }
        }
    }
}