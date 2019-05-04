using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Topicos.Models
{
    public class ContextTopicos
    {
        public IMongoDatabase Database;
        public string DataBaseName = "ClienteDB";
        string conexaoMongoDB = "";

        public ContextTopicos()
        {
            conexaoMongoDB = ConfigurationManager.ConnectionStrings["conexaoMongoDB"].ConnectionString;
            var client = new MongoClient(conexaoMongoDB);

            Database = client.GetDatabase(DataBaseName);
        }

        public IMongoCollection<ProdutoModel> ProdutosDB
        {
            get
            {
                var Produtos = Database.GetCollection<ProdutoModel>("Produtos");
                return Produtos;
            }
        }

        public IMongoCollection<UsuarioModel> UsuariosDB
        {
            get
            {
                var Usuarios = Database.GetCollection<UsuarioModel>("Usuarios");
                return Usuarios;
            }
        }

        public IMongoCollection<CarrinhoModel> CarrinhoDB
        {
            get
            {
                var Carrinhos = Database.GetCollection<CarrinhoModel>("Carrinhos");
                return Carrinhos;
            }
        }

        public IMongoCollection<VendaModel> VendaDB
        {
            get
            {
                var Vendas = Database.GetCollection<VendaModel>("Vendas");
                return Vendas;
            }
        }
    }
}