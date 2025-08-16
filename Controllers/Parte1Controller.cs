using Microsoft.AspNetCore.Mvc;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	/// <summary>
	/// Ao rodar o código abaixo o serviço deveria sempre retornar um número diferente, mas ele fica retornando sempre o mesmo número.
	/// 1 - Faça as alterações para que o retorno seja sempre diferente
	/// 2 - Tome cuidado 
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte1Controller :  ControllerBase
	{
		private readonly RandomService _randomService;

		public Parte1Controller(RandomService randomService)
		{
			_randomService = randomService;
		}
		[HttpGet]
		public async Task<int> Index()
		{
			return await _randomService.GetRandom();
		}
	}
}


//1 - Problema do número repetido
//O problema ocorria porque o serviço estava configurado como singleton, o que fazia com que a instância fosse criada apenas uma vez durante o ciclo de vida da aplicação. Como a seed do gerador aleatório era criada no construtor da classe, havia apenas uma seed fixa durante toda a execução.

//Assim, o número gerado era sempre o mesmo. A solução foi alterar o tempo de vida do serviço para scoped, garantindo uma nova instância a cada requisição.

//Importante ressaltar que essa não é a única solução. Poderíamos, por exemplo, gerar a seed dentro do próprio método, em tempo de request. No entanto, optei por usar o serviço com tempo de vida por escopo, pois isso permite aproveitar a injeção de dependência do DbContext.

//2 - Erro de inserção duplicada
//O segundo problema refere-se à exceção lançada ao tentar salvar números duplicados no banco de dados. Isso ocorre porque há um índice único definido no campo "Number" nas migrations, impedindo duplicações.

//Uma solução mais simples seria remover o índice único, permitindo valores repetidos.

//Contudo, se a intenção for manter a unicidade, é necessário tratar a exceção ao tentar inserir um valor já existente. Nesse caso, podemos capturar a exceção específica e retornar um erro 400 (Bad Request) informando ao cliente que o número já foi gerado anteriormente.
