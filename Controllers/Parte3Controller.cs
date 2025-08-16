using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models.Response;
using ProvaPub.Services.Interfaces;
using ProvaPub.Services.PaymentMethods;

namespace ProvaPub.Controllers
{

    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
    /// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
    /// 
    /// Outra parte importante é em relação à data (OrderDate) do objeto Order. Ela deve ser salva no banco como UTC mas deve retornar para o cliente no fuso horário do Brasil. 
    /// Demonstre como você faria isso.
    /// </summary>
    [ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
	{
        private readonly IOrderService _orderService;

        public Parte3Controller(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders")]
		public async Task<OrderCreatedResponse> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
            var paymentService = PaymentMethodFactory.CreatePaymentService(paymentMethod);

            return await _orderService.PayOrder(paymentService, paymentValue, customerId);
		}
	}
}

//1 - Refatoração do fluxo de pagamento
//Para tornar a arquitetura mais escalável e desacoplada, foram utilizados os seguintes padrões:

//Factory (Criacional):
//Uma fábrica foi criada para instanciar dinamicamente o serviço de pagamento com base no método informado (ex: "pix", "paypal"). Isso centraliza e isola a lógica de criação dos serviços.

//Strategy (Comportamental):
//Cada método de pagamento implementa a interface IPaymentService, permitindo que o serviço de pedidos (OrderService) utilize qualquer implementação sem conhecer detalhes do pagamento. Assim, o comportamento (estratégia) pode ser alterado em tempo de execução, sem modificar o código cliente.

//Essa combinação de padrões garante que a arquitetura esteja aberta para extensão e fechada para modificação, atendendo ao princípio OCP.

//2 - Criação de um pedido e ajuste no retorno da data

//2 - Injeção de Dependência
//O campo OrderDate da entidade Order é salvo sempre em UTC, garantindo consistência no armazenamento

//Para a resposta ao cliente, foi criado um model de resposta (DTO) que transforma o OrderDate para o horário local desejado.

//Caso o retorno deva refletir o fuso do servidor, a conversão pode ser feita com base no horário local da máquina.
