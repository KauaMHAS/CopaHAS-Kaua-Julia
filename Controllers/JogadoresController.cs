using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaHAS.Models;
using CopaHAS.Models.Enuns;
using Microsoft.AspNetCore.Mvc;

namespace CopaHAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogadoresExercicioController : ControllerBase
    {
        private static List<Jogador> listaJogadores = new List<Jogador>()
        {
            new Jogador(){Id = 1, Nome ="Hugo Souza", NumeroCamisa = 1, Posicao = "Goleiro", Status = StatusJogador.Titular},
            new Jogador(){Id = 2, Nome ="Neymar Junior", NumeroCamisa = 10, Posicao = "Atacante", Status = StatusJogador.Titular},
            new Jogador(){Id = 3, Nome ="Alisson Becker", NumeroCamisa = 23, Posicao = "Goleiro", Status = StatusJogador.Reserva},
            new Jogador(){Id = 4, Nome ="Marquinhos", NumeroCamisa = 4, Posicao = "Zagueiro", Status = StatusJogador.Titular},
            new Jogador(){Id = 5, Nome ="Thiago Silva", NumeroCamisa = 3, Posicao = "Zagueiro", Status = StatusJogador.Reserva},
            new Jogador(){Id = 6, Nome ="Casemiro", NumeroCamisa = 5, Posicao = "Volante", Status = StatusJogador.Titular},
            new Jogador(){Id = 7, Nome ="Lucas Paquetá", NumeroCamisa = 8, Posicao = "Meio-Campo", Status = StatusJogador.Titular},
            new Jogador(){Id = 8, Nome ="Vinicius Junior", NumeroCamisa = 7, Posicao = "Atacante", Status = StatusJogador.Titular},
            new Jogador(){Id = 9, Nome ="Rodrygo", NumeroCamisa = 11, Posicao = "Atacante", Status = StatusJogador.Reserva},
            new Jogador(){Id = 10, Nome ="Richarlison", NumeroCamisa = 9, Posicao = "Centroavante", Status = StatusJogador.Titular},
            new Jogador(){Id = 11, Nome ="Danilo", NumeroCamisa = 2, Posicao = "Lateral Direito", Status = StatusJogador.Titular},
            new Jogador(){Id = 12, Nome ="Alex Sandro", NumeroCamisa = 6, Posicao = "Lateral Esquerdo", Status = StatusJogador.Reserva}
        };

        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            List<Jogador> listaBusca = listaJogadores.FindAll(j => j.Nome.ToLower().Contains(nome.ToLower()));

            if (!listaBusca.Any())
                return NotFound("Jogador não encontrado");

            return Ok(listaBusca);
        }

        [HttpGet("GetTitulares")]
        public IActionResult GetTitulares()
        {
            List<Jogador> lista = listaJogadores
                .Where(j => j.Status == StatusJogador.Titular)
                .OrderByDescending(j => j.NumeroCamisa)
                .ToList();

            return Ok(lista);
        }

        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            int totalJogadores = listaJogadores.Count;
            int somaCamisas = listaJogadores.Sum(j => j.NumeroCamisa);

            return Ok("Quantidade de Jogadores: " + totalJogadores + "\n" + "Somatorio das Camisas: " + somaCamisas);
        }

        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(Jogador j)
        {
            if (j.NumeroCamisa > 100)
                return BadRequest("O numero da camisa nao pode ser maior que 100");

            listaJogadores.Add(j);
            return Ok(listaJogadores);
        }

        [HttpPost("PostValidacaoNome")]
        public IActionResult PostValidacaoNome(Jogador j)
        {
            if (j.Posicao != "Goleiro" && j.NumeroCamisa == 1)
                return BadRequest("Apenas o goleiro pode usar a camisa numero 1");

            listaJogadores.Add(j);
            return Ok(listaJogadores);
        }

        [HttpGet("GetByStatus/{status}")]
        public IActionResult GetByStatus(int status)
        {
            List<Jogador> lista = listaJogadores
                .FindAll(j => j.Status == (StatusJogador)status);

            return Ok(lista);
        }
    }
}