using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	
	/// <summary>
	/// O Código abaixo faz uma chmada para a regra de negócio que valida se um consumidor pode fazer uma compra.
	/// Crie o teste unitário para esse Service. Se necessário, faça as alterações no código para que seja possível realizar os testes.
	/// Tente criar a maior cobertura possível nos testes.
	/// 
	/// Utilize o framework de testes que desejar. 
	/// Crie o teste na pasta "Tests" da solution
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte4Controller :  ControllerBase
	{
		private readonly CustomerService _customerService;

        public Parte4Controller(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("CanPurchase")]
		public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
		{
			return await _customerService.CanPurchase(customerId, purchaseValue, DateTime.UtcNow);
		}
	}
}

//1 - Testes e mudança no método "CanPurchase"

//O método CanPurchase foi refatorado para receber a data como parâmetro, substituindo o uso direto de DateTime.UtcNow, o que permite maior controle nos testes.

//Para utilização dos teste dentro do contexto de uso de dbcontext foi adotado o uso do UseInMemoryDatabase, que permite criar uma instância real do banco em memória, garantindo execução real do EF Core sem a necessidade de banco físico.

//Isso possibilitou a criação de testes confiáveis e eficientes, cobrindo todas as regras de negócio do método, como validação de cliente, limite de compras mensais, valor máximo na primeira compra e horário comercial.