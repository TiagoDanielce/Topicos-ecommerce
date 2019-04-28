using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Topicos.Models;

namespace Topicos.Controllers
{
    public class ProdutoController : Controller
    {
        readonly ContextTopicos db = new ContextTopicos();
        //readonly string pathImages = "~/App_Data/Images/Produtos";
        readonly string pathImages = "~/Images/Produtos";
        // GET: Produto
        public ActionResult Index()
        {
            ViewBag.Admin = true;
            ViewBag.ExibeFooter = false;
            //var x = new ProdutoModel()
            //{
            //    Categoria = CategoriaProduto.Hardware,
            //    Descricao = "Esse é um registro de teste",
            //    Titulo = "Produto teste",
            //    Preco = 20.00M
            //};
            //db.ProdutosDB.InsertOne(x);

            var list = db.ProdutosDB.Find(p => true).ToList();
            return View(list);
        }

        // GET: Produto/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Admin = true;
            ViewBag.ExibeFooter = true;

            if (!string.IsNullOrEmpty(id))
            {
                var produto = db.ProdutosDB.Find(p => p.Id == id).FirstOrDefault();

                var dir = new DirectoryInfo(Server.MapPath("~/Images/Produtos/"));
                FileInfo[] fileNames = dir.GetFiles("*.*");
                List<string> items = new List<string>();
                foreach (var file in fileNames)
                {
                    if (file.Name.Contains(id))
                        items.Add(file.Name);
                }
                ViewBag.Imagens = items;

                return View(produto);
            }

            return RedirectToAction("Index", "Home"); ;
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Admin = true;
            ViewBag.ExibeFooter = false;
            var produto = db.ProdutosDB.Find(p => p.Id == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(id))
                return View(produto);

            return View();
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, ProdutoModel model, IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    model.Id = id;
                    var filter = Builders<ProdutoModel>.Filter.Eq(p => p.Id, id);
                    db.ProdutosDB.ReplaceOne(filter, model);
                }
                else
                    db.ProdutosDB.InsertOne(model);

                var c = 1;
                foreach (var file in files)
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = model.Id + "_" + c + ".jpg";
                        var path = Path.Combine(Server.MapPath(pathImages), fileName);
                        file.SaveAs(path);
                        c++;
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                db.ProdutosDB.FindOneAndDelete(p => p.Id == id);

                //Exclui arquivos do produto
                for (int i = 1; i <= 3; i++)
                {
                    var fileSalvo = Directory.GetFiles(pathImages, id + "_" + i + "*.*");
                    foreach (var item in fileSalvo)
                        System.IO.File.Delete(item);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult BuscaImagens(string id)
        {
            var dir = new DirectoryInfo(Server.MapPath("~/Images/Produtos/"));
            FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                if (file.Name.Contains(id))
                    items.Add(file.Name);
            }
            return View(items);
        }
        public FileResult Download(string ImageName)
        {
            var FileVirtualPath = "~/Images/Produtos/" + ImageName;
            return File(FileVirtualPath, "application / force - download", Path.GetFileName(FileVirtualPath));
        }
    }
}
