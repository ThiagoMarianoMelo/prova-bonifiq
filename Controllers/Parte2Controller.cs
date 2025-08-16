using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
		private readonly ProductService _productService;
        private readonly CustomerService _customerService;

        public Parte2Controller(ProductService productService, CustomerService customerService)
        {
            _productService = productService;
            _customerService = customerService;
        }

        /// <summary>
        /// Precisamos fazer algumas alterações:
        /// 1 - Não importa qual page é informada, sempre são retornados os mesmos resultados. Faça a correção.
        /// 2 - Altere os códigos abaixo para evitar o uso de "new", como em "new ProductService()". Utilize a Injeção de Dependência para resolver esse problema
        /// 3 - Dê uma olhada nos arquivos /Models/CustomerList e /Models/ProductList. Veja que há uma estrutura que se repete. 
        /// Como você faria pra criar uma estrutura melhor, com menos repetição de código? E quanto ao CustomerService/ProductService. Você acha que seria possível evitar a repetição de código?
        /// 
        /// </summary>


        [HttpGet("products")]
		public PaginationModel<Product> ListProducts(int page)
		{
			return _productService.ListProducts(page);
		}

		[HttpGet("customers")]
		public PaginationModel<Customer> ListCustomers(int page)
		{
			return _customerService.ListCustomers(page);
		}
	}
}


//1 - Correção da Paginação
//O problema era a ausência de lógica de paginação, sempre retornava todos os registros. Corrigi usando Skip e Take com base na página informada.

//Para calcular HasNext, busco 11 itens: se existir um extra, há próxima página. Essa abordagem evita o uso de Count(), reduzindo a carga no banco.

//2 - Injeção de Dependência
//O controller criava os serviços manualmente com new. Corrigi usando injeção de dependência.

//Um ponto importante que o ideal aqui seria utilizar de interfaces para esses seriços, permitindo que a classe controler conheça somente o contrato e siga o principio da inversão de dependência

//3 - Redução de Repetição (Models e Serviços)
//Foi criado uma estrutura de model para paginação que recebe a entidade que será paginada, tornando desnecessária dois models com dados repetidos

//Foi criado uma classe base para serviços que realizam a paginação, sendo assim o serviço de product e customer podem herdar e reaproveitar o código da classe pai, permitindo remoção de código duplicado, porém aumentando levemnte a complexidade e acoplamento