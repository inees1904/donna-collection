using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pap.User
{
    public class Pedido
    {
        public int DetalhesPedidoId { get; set; }
        public string PedidoN { get; set; }
        public int UserId { get; set; }
        public int Estado { get; set; }
        public DateTime DataPedido { get; set; }
        public bool IsCancel { get; set; }
        public decimal Total { get; set; }
        public string MetodoPagamento { get; set; }
        public string NomeFaturacao { get; set; }
        public string EmailFaturacao { get; set; }
        public string TelemovelFaturacao { get; set; }
        public string MoradaFaturacao { get; set; }
        public string CodPostalFaturacao { get; set; }
        public string NifFaturacao { get; set; }
        public string NomeEnvio { get; set; }
        public string TelemovelEnvio { get; set; }
        public string MoradaEnvio { get; set; }
        public string CodPostalEnvio { get; set; }
        public string NifEnvio { get; set; }
        public string LinkRastreio { get; set; }
        public List<PedidoItem> Itens { get; set; } 

        public Pedido()
        {
            Itens = new List<PedidoItem>();
        }
    }

    public class PedidoItem
    {
        public int PedidoItemId { get; set; }
        public int DetalhesPedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}