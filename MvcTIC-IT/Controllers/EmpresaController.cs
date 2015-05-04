using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTIC_IT.Models;
using MvcTIC_IT.Libraries;

using System.IO;
using PagedList;


namespace MvcTIC_IT.Controllers
{
    public class EmpresaController : BaseController
    {



        ServiceRefPortugal.ServicePortugalClient proxy = new ServiceRefPortugal.ServicePortugalClient();
        ApagarFotosNoDisco apagafoto = new ApagarFotosNoDisco();
 



        //
        // GET: /Empresa/
        [Authorize(Roles = "admin,user")]
        public ActionResult Index(int page = 1)
        {

            // I do this just in case someone tries to put in 0 or a negative number
            if (page < 1)
            {
                page = 1;
            }   

            //var index = proxy.GetEmpresasIndexByIdentidade(User.Identity.Name);

            //return View("Index", index);

            List<MvcTIC_IT.ServiceRefPortugal.empresa> products = proxy.GetEmpresasIndexByIdentidade(User.Identity.Name).ToList();
            IPagedList<MvcTIC_IT.ServiceRefPortugal.empresa> productPage = products.ToPagedList(page, 10);

            return View(productPage);
        }

        //
        // GET: /Empresa/Details/5

        public ActionResult Details(int id = 0)
        {
            //empresa empresa = db.empresa.Single(e => e.id == id);

            MvcTIC_IT.ServiceRefPortugal.empresa empresa = proxy.GetEmpresaIndexByID(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        //
        // GET: /Empresa/Create
        [Authorize(Roles = "admin,user")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Empresa/Create
        [Authorize(Roles = "admin,user")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(MvcTIC_IT.ViewModels.EmpresaViewmodel empresa)
        {
            if (ModelState.IsValid)
            {

                MvcTIC_IT.ServiceRefPortugal.EmpresaModel pproc = new MvcTIC_IT.ServiceRefPortugal.EmpresaModel();

                pproc.Contacto = empresa.contacto;
                pproc.Logo = empresa.logo;
                pproc.Nome_da_empresa = empresa.nome_da_empresa;
                pproc.Url = empresa.url;
                pproc.Username = User.Identity.Name;

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase postedFile = Request.Files[fileName];
                    var file = Request.Files[fileName];
                    string fileNamev1 = System.IO.Path.GetFileName(file.FileName);


                    string fileext = System.IO.Path.GetExtension(file.FileName);
                    int idproc = 0;
                    if (fileNamev1 == "")
                    {
                        pproc.Logo = "nologo.jpg";
                        idproc = proxy.AddEmpresa(pproc);

                        proxy.UpdateImage_file(idproc, pproc.Logo);
                    }
                    else
                    {

                        //nova imagem
                        settings set = new settings();
                        string imagesDir = set.logo;

                        //add empresa
                        idproc = proxy.AddEmpresa(pproc);
                        ///guarda nome na base de dados retorna ID

                        //UploadImage
                        pproc.Id = idproc;
                        pproc.Logo = idproc + fileext;
                        proxy.Upload(pproc.Logo, file.InputStream);

                        proxy.UpdateImage_file(idproc, pproc.Logo);

                    }

                }




                return RedirectToAction("Index");


            }

            return View(empresa);
        }

        //
        // GET: /Empresa/Edit/5
             [Authorize(Roles = "admin,user")]
        public ActionResult Edit(int id = 0)
        {
         

            MvcTIC_IT.ServiceRefPortugal.empresa empresa = proxy.GetEmpresaIndexByID(id);

            MvcTIC_IT.ViewModels.EmpresaViewmodel emp=new ViewModels.EmpresaViewmodel();

            emp.contacto = empresa.contacto;
            emp.nome_da_empresa = empresa.nome_da_empresa;
            emp.url = empresa.url;
            emp.id = empresa.id;
            emp.logo = empresa.logo;
          

            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        //
        // POST: /Empresa/Edit/5
        [Authorize(Roles = "admin,user")]
        [HttpPost]
        public ActionResult Edit(int id,MvcTIC_IT.ViewModels.EmpresaViewmodel empresa)
        {
            if (ModelState.IsValid)
            {
                MvcTIC_IT.ServiceRefPortugal.EmpresaModel pproc = new MvcTIC_IT.ServiceRefPortugal.EmpresaModel();

                pproc.Contacto = empresa.contacto;
                //pproc.Logo = empresa.logo;
                pproc.Nome_da_empresa = empresa.nome_da_empresa;
                pproc.Url = empresa.url;
                pproc.Username = User.Identity.Name;

                proxy.UpdateEmpresa(id, pproc);



                return RedirectToAction("Index");
            }
            return View(empresa);
        }

        //
        // GET: /Empresa/Delete/5
             [Authorize(Roles = "admin,user")]
        public ActionResult Delete(int id = 0)
        {
            //empresa empresa = db.empresa.Single(e => e.id == id);
            MvcTIC_IT.ServiceRefPortugal.empresa emp= proxy.GetEmpresaIndexByID(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        //
        // POST: /Empresa/Delete/5
             [Authorize(Roles = "admin,user")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //empresa empresa = db.empresa.Single(e => e.id == id);
            //db.empresa.DeleteObject(empresa);
            //db.SaveChanges();

            try
            {
              
                proxy.DeleteEmpresa(id);
                
                return View("Deleted");
            }
            catch
            {
                return View("Error");
            }


            ////return RedirectToAction("Index");
        }

   
     


        public ActionResult ThumbImoveis(string id)
        {
            return new ThumbImoveisResult(id);
        }


        [Authorize(Roles = "admin,user")]
        [ValidateInput(false)]
        public ActionResult InsereFotos(MvcTIC_IT.ServiceRefPortugal.empresa cat)
        {
            ViewData["idi"] = cat.id;


            cat = proxy.GetEmpresaIndexByID(cat.id);


            MvcTIC_IT.ServiceRefPortugal.EmpresaModel pproc = new MvcTIC_IT.ServiceRefPortugal.EmpresaModel();

            
  

            foreach (string name in Request.Files)
            {
                var file = Request.Files[name];

                string fileName = System.IO.Path.GetFileName(file.FileName);               
                string fileext = System.IO.Path.GetExtension(file.FileName);


                if (fileName == "")
                {
                    pproc.Logo = "nologo.jpg";
                    //proxy.UpdateEmpresa(cat.id, pproc);
                    proxy.UpdateImage_file(cat.id, pproc.Logo);
                    cat.logo = pproc.Logo;
                }
                else
                {
                    

                    //nova imagem
                    settings set = new settings();
                    string imagesDir = set.logo;


                    pproc.Id = cat.id;
                    pproc.Logo = cat.id + fileext;

                    //add empresa
                    //proxy.UpdateEmpresa(cat.id, pproc);
                    ///guarda nome na base de dados retorna ID

                    //UploadImage

                    proxy.Upload(pproc.Logo, file.InputStream);



                    proxy.UpdateImage_file(cat.id, pproc.Logo);

                    cat.logo = pproc.Logo;
                }

                return View("InsereFotos", cat);
            }
            return View("InsereFotos", cat);
        }



    }
}