using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTIC_IT.ViewModels;
using MvcTIC_IT.Libraries;
using PagedList;

namespace MvcTIC_IT.Controllers
{
    public class PortugalController : BaseController
    {
        //
        // GET: /Portugal/

        ServiceRefPortugal.ServicePortugalClient proxy =new ServiceRefPortugal.ServicePortugalClient();

        [Authorize(Roles = "admin,user")]
        public ActionResult MeuAdmin()
        {

            return View();

        }


        [Authorize(Roles = "admin")]
        public ActionResult Index_Admin()
        {



            //get idioma
            string lang = RouteData.Values["lang"].ToString();
            int idiomacode = proxy.GetIdioma_ID(lang);


            var imo = proxy.GetAllAnunciosID(RouteData.Values["lang"].ToString()).ToList();

            ViewData["totalregistos"] = imo.Count();

            return View("IndexAdmin", imo);

        }

        public ActionResult Index(int page = 1)
        {
            // I do this just in case someone tries to put in 0 or a negative number
            if (page < 1) 
            {       
                page = 1;    
            }     
        
            List<MvcTIC_IT.ServiceRefPortugal.PortugalViewModel> products = proxy.GetAllAnunciosID(RouteData.Values["lang"].ToString()).ToList();
            IPagedList<MvcTIC_IT.ServiceRefPortugal.PortugalViewModel> productPage = products.ToPagedList(page, 10);

            return View(productPage);
        }
        
            
            
            
            public ActionResult Page() {


            //MvcTIC_IT.ServiceRefPortugal.PortugalViewModel model = new ServiceRefPortugal.PortugalViewModel();
                MvcTIC_IT.ServiceRefPortugal.PortugalViewModel model = new ServiceRefPortugal.PortugalViewModel();
            //List<Product> products = CatalogService.ListOpenProducts();
                List<MvcTIC_IT.ServiceRefPortugal.PortugalViewModel> products = proxy.GetAllAnunciosID(RouteData.Values["lang"].ToString()).ToList();
            //PagedList<MvcTIC_IT.ServiceRefPortugal.PortugalViewModel> data = new PagedList<MvcTIC_IT.ServiceRefPortugal.PortugalViewModel>(products, id, 2);
 

            var pageIndex = model.Page ?? 1;
            model.SearchResults = products.ToPagedList(pageIndex, 10);


            return View(model); 
        
        }

        [Authorize(Roles = "admin,user")]
        public ActionResult IndexMeuAdmin(int page = 1)
        {
            // I do this just in case someone tries to put in 0 or a negative number
            if (page < 1)
            {
                page = 1;
            }

            List<MvcTIC_IT.ServiceRefPortugal.PortugalViewModel> products = proxy.GetAnunciosIndexByIdentidade(User.Identity.Name, RouteData.Values["lang"].ToString()).ToList();
            IPagedList<MvcTIC_IT.ServiceRefPortugal.PortugalViewModel> productPage = products.ToPagedList(page, 10);

            return View(productPage);
        }


        public ActionResult PageMeu()
        {


            //MvcTIC_IT.ServiceRefPortugal.PortugalViewModel model = new ServiceRefPortugal.PortugalViewModel();
            MvcTIC_IT.ServiceRefPortugal.PortugalViewModel model = new ServiceRefPortugal.PortugalViewModel();
            //List<Product> products = CatalogService.ListOpenProducts();
            List<MvcTIC_IT.ServiceRefPortugal.PortugalViewModel> products = proxy.GetAnunciosIndexByIdentidade(User.Identity.Name, RouteData.Values["lang"].ToString()).ToList();
            //PagedList<MvcTIC_IT.ServiceRefPortugal.PortugalViewModel> data = new PagedList<MvcTIC_IT.ServiceRefPortugal.PortugalViewModel>(products, id, 2);


            var pageIndex = model.Page ?? 1;
            model.SearchResults = products.ToPagedList(pageIndex, 10);


            return View(model);

        }

        [Authorize(Roles = "admin,user")]
        public ActionResult CriarOferta()
        {

            ViewBag.empresa_id = new SelectList(proxy.GetAllEmpresaByID(User.Identity.Name), "id", "nome_da_empresa");

            return View();
        }

        [Authorize(Roles = "admin,user")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CriarOferta(MvcTIC_IT.ViewModels.PortugalViewModel collection)
        {

            try
            {

                collection.dataanuncio = DateTime.Now;
                collection.identidade = User.Identity.Name;

             


                MvcTIC_IT.ServiceRefPortugal.AnuncioModel pproc = new MvcTIC_IT.ServiceRefPortugal.AnuncioModel();


                pproc.Dataanuncio = collection.dataanuncio;
                pproc.Email = collection.email;
                pproc.Identidade = collection.identidade;
                pproc.Numvisitas = 0;
                pproc.Salario = collection.salario;
                pproc.Local_de_trabalho = collection.local_de_trabalho;
                if (collection.empresa_id == 0)
                {
                    pproc.EmpresaId = 19;
                    //pproc.EmpresaId = collection.empresa_id;
                    //return View("TemdeCriarEmpresa");
                    //return RedirectToAction("Create","Empresa");
                }
                else
                {
                    pproc.EmpresaId = collection.empresa_id;
                
                }
               

              

                int idproc = proxy.AddAnuncioModel(pproc);

                if (idproc == -1)
                {
                    return View("Error");
                }

                ////////////////////////////////////////////////////////////////////////////////////
                ////get idioma /////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////
                string lang = RouteData.Values["lang"].ToString();


                MvcTIC_IT.ServiceRefPortugal.DescricaoModel desmo = new MvcTIC_IT.ServiceRefPortugal.DescricaoModel();

                desmo.Descricao = collection.descricao;

                //desmo.Descricao = HttpUtility.HtmlEncode(collection.descricao);
         
                desmo.Titulo = collection.titulo;
                desmo.Idioma_id = proxy.GetIdioma_ID(lang);
                desmo.Anuncio_id = idproc;


                if (!proxy.AddDescricao(desmo))
                {

                    return View("Error");
                }

                string body = "Anuncio criado por : " + User.Identity.Name + " na data : " + DateTime.Now;

                Utils.sendSimpleEmail("pedro@dinispt.com", "JOBS", "pedro@dinispt.com", "JOBS-Portugal -- Anuncio Criado", body);

                return View("Gravado");
            }
            catch
            {
                return View("Error");
            }


        }


        public ActionResult Detalhes(int id)
        {

            proxy.UpdateVisitasAnuncioPortugal(id);

            string lang = RouteData.Values["lang"].ToString();

            var i = proxy.GetAnunciosProcuraIndexByID(id);

            var desc = proxy.GetAnunciosDescProcuraIndexByIDAndLangCode(id, lang);

            //if (desc == null)
            //{
            //    //procurar alternativa
            //    //contar quantas traducoes existem
            //    int numero = proxy.GetCountIdiomas(id);

            //    if (numero == 0)
            //    {
            //        return View("Error");
            //    }

            //    //faz um get para saber que idiomas existem para alem do activo
            //    var descall = proxy.GetAnunciosDescProcuraIndexByIDALL(id);

            //    foreach (var idi in descall)
            //    {
            //        if (idi.idiomas.lang_code == "en-US")
            //        {
            //            desc = idi;
            //            break;
            //        }
            //        if (idi.idiomas.lang_code == "pt-PT")
            //        {
            //            desc = idi;
            //            break;
            //        }

            //    }
            //}



            MvcTIC_IT.ServiceRefPortugal.PortugalViewModel oi = new MvcTIC_IT.ServiceRefPortugal.PortugalViewModel();
            oi.id = i.id;
            oi.email = i.email;            
            oi.dataanuncio = i.dataanuncio;
            oi.local_de_trabalho = i.local_de_trabalho;
            oi.salario = i.salario;

            oi.empresa_id = i.empresa_id;

            if (desc != null)
            {

                oi.descricao = desc.descricao;
                oi.titulo = desc.titulo;
            }
            
            oi.numvisitas = (int)i.numvisitas;

            return View("detalhes", oi);
        }

        public ActionResult ThumbImoveis(string id)
        {
            return new ThumbImoveisResult(id);
        }



        [Authorize(Roles = "admin,user")]
        public ActionResult Edit(int id = 0)
        {

            //id = 7;
            string lang = RouteData.Values["lang"].ToString();

            //ViewBag.empresa_id = new SelectList(proxy.GetAllEmpresaByID(User.Identity.Name), "id", "nome_da_empresa");

            var i = proxy.GetAnunciosProcuraIndexByID(id);

            var desc = proxy.GetAnunciosDescProcuraIndexByIDAndLangCode(id, lang);

            ViewBag.empresa_id = new SelectList(proxy.GetEmpresasIndexByIdentidade(User.Identity.Name), "id", "nome_da_empresa", i.empresa_id);

           



            MvcTIC_IT.ViewModels.PortugalViewModel oi = new MvcTIC_IT.ViewModels.PortugalViewModel();
            oi.id = i.id;
            oi.email = i.email;
            oi.dataanuncio = i.dataanuncio;
            oi.local_de_trabalho = i.local_de_trabalho;
            oi.salario = i.salario;

            oi.empresa_id = i.empresa_id;

            if (desc != null)
            {

                oi.descricao = desc.descricao;
                oi.titulo = desc.titulo;
            }

            oi.numvisitas = (int)i.numvisitas;



            if (oi == null)
            {
                return HttpNotFound();
            }
            return View("Editar", oi);

    
        }

        [Authorize(Roles = "admin,user")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, MvcTIC_IT.ViewModels.PortugalViewModel collection, string hidden_field_id)
        {

            string lang = RouteData.Values["lang"].ToString();


            if (ModelState.IsValid)
            {
                MvcTIC_IT.ServiceRefPortugal.AnuncioModel pproc = new MvcTIC_IT.ServiceRefPortugal.AnuncioModel();

                pproc.Dataanuncio = DateTime.Now;
                pproc.Email = collection.email;
                //pproc.Identidade = collection.identidade;
                //pproc.Numvisitas = 0;
                pproc.Salario = collection.salario;
                pproc.Local_de_trabalho = collection.local_de_trabalho;
                if (collection.empresa_id == 0)
                {
                    pproc.EmpresaId = 19;
                    //pproc.EmpresaId = collection.empresa_id;
                    //return View("TemdeCriarEmpresa");
                    //return RedirectToAction("Create","Empresa");
                }
                else
                {
                    pproc.EmpresaId = collection.empresa_id;

                }


                MvcTIC_IT.ServiceRefPortugal.DescricaoModel pprocdesc = new MvcTIC_IT.ServiceRefPortugal.DescricaoModel();

                pprocdesc.Descricao = collection.descricao;
                pprocdesc.Titulo = collection.titulo;

                proxy.UpdateAnuncio(id, pproc, pprocdesc, lang);



                //db.empresa.Attach(empresa);
                //db.ObjectStateManager.ChangeObjectState(empresa, EntityState.Modified);
                //db.SaveChanges();
                return RedirectToAction("IndexMeuAdmin");
            }
            return View("Editar", collection);
        }



        [Authorize(Roles = "user,admin")]
        [ValidateInput(false)]
        public ActionResult Globalizar(int id)
        {


            string lang = RouteData.Values["lang"].ToString();

            var desc = proxy.GetAnunciosDescProcuraIndexByIDAndLangCode(id, lang);

            ////se a descricao for null quer dizer que nao existe traducao para o idioma activo
            //if (desc == null)
            //{
            //    //procurar alternativa
            //    //contar quantas traducoes existem
            //    int numero = proxy.GetCountIdiomas(id);

            //    if (numero == 0)
            //    {
            //        return View("Error");
            //    }

            //    //faz um get para saber que idiomas existem para alem do activo
            //    var descall = proxy.GetAnunciosDescProcuraIndexByIDALL(id);

            //    foreach (var idi in descall)
            //    {
            //        if (idi.idiomas.lang_code == "en-US")
            //        {
            //            desc = idi;
            //            break;
            //        }
            //        if (idi.idiomas.lang_code == "pt-PT")
            //        {
            //            desc = idi;
            //            break;
            //        }

            //    }


            //}


            var iml = proxy.GetAnunciosProcuraIndexByID(id);
            if (iml == null)
            {
                return View("Error");
              
            }
            else
            {


                //MvcTIC_IT.ServiceRefPortugal.PortugalViewModel procview = new MvcTIC_IT.ServiceRefPortugal.PortugalViewModel();
                MvcTIC_IT.ViewModels.PortugalViewModel procview = new MvcTIC_IT.ViewModels.PortugalViewModel();
                if (desc == null)
                {
                    procview.descricao = null;
                    procview.titulo = null;
             
                }
                else
                {
                    procview.descricao = desc.descricao;
                    procview.titulo = desc.titulo;
                }
                return View("Globalizar", procview);

            }


           
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "user,admin")]
        [ValidateInput(false)]
        public ActionResult Globalizar(int id, MvcTIC_IT.ViewModels.PortugalViewModel collection)
        {

            try
            {


                var iml = proxy.GetAnunciosProcuraIndexByID(id);
                if (iml == null)
                    return View("Error");



                string lang = RouteData.Values["lang"].ToString();

                var imodesc = proxy.GetAnunciosDescProcuraIndexByIDAndLangCode(id, lang);

                MvcTIC_IT.ServiceRefPortugal.DescricaoModel pprocdesc = new MvcTIC_IT.ServiceRefPortugal.DescricaoModel();

                if (imodesc == null)
                {
                    pprocdesc.Descricao = collection.descricao;
                    pprocdesc.Titulo = collection.titulo;
                    pprocdesc.Idioma_id = proxy.GetIdioma_ID(lang);
                    pprocdesc.Anuncio_id = id;


                    if (!proxy.AddDescricao(pprocdesc))
                    {
                        return View("Error");
                    }
                    else
                    {
                        return View("Gravado");
                    }



                }
                else
                {

                    if ((collection.descricao == null) || (collection.titulo == null))
                    {
                        return View("Globalizar", collection);

                    }
                    else
                    {
                        pprocdesc.Descricao = collection.descricao;
                        pprocdesc.Titulo = collection.titulo;
                        try
                        {
                            proxy.UpdateAnuncioDescricao(id, pprocdesc, lang);
                        }
                        catch
                        {

                            return View("Error");
                        }
                        return View("Gravado");
                    }
                }


                
            }
            catch
            {
                return View("Error");
            }

       
      }


        [Authorize(Roles = "admin,user")]
        [ValidateInput(false)]
        public ActionResult Apagar(int id)
        {

            var cat = proxy.GetAnunciosProcuraIndexByID(id);

            

            if (cat == null)
                return View("Error");
            else
                return View("Apagar", cat);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "admin,user")]
        [ValidateInput(false)]
        public ActionResult Apagar(int id, string confirmButton)
        {
            var cat = proxy.GetAnunciosProcuraIndexByID(id);
            if (cat == null)
                return View("Error");

            try
            {

               

                proxy.DeleteAnuncio(id);
                proxy.Save();

                return View("Deleted");
            }
            catch
            {
                return View("Error");
            }
        }


        public ActionResult Pesquisa()
        {
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Pesquisa(MvcTIC_IT.ServiceRefPortugal.SearchPortugalViewModel collection,int page = 1)
        {


            // I do this just in case someone tries to put in 0 or a negative number
            if (page < 1)
            {
                page = 1;
            }


            try
            {
                string lang = RouteData.Values["lang"].ToString();
                int idiomacode = proxy.GetIdioma_ID(lang);
                List<MvcTIC_IT.ServiceRefPortugal.SearchPortugalViewModel> index = proxy.Search(collection, idiomacode).ToList();
                IPagedList<MvcTIC_IT.ServiceRefPortugal.SearchPortugalViewModel> productPage = index.ToPagedList(page, 10);
                //ViewData["totalregistos"] = index.Count();
                return View("IndexPesquisa", productPage);
            }
            catch
            {
                return View("Error");
            }
        }





    }
}
