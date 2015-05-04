using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcTIC_IT.Libraries;

namespace MvcTIC_IT.Controllers
{
    public class ApagaContaController : BaseController
    {


        ServiceRefAlemanha.ServiceAlemanhaClient proxyde = new ServiceRefAlemanha.ServiceAlemanhaClient();
        ServiceRefEspanha.ServiceEspanhaClient proxyes = new ServiceRefEspanha.ServiceEspanhaClient();
        ServiceRefPortugal.ServicePortugalClient proxypt = new ServiceRefPortugal.ServicePortugalClient();
        ServiceRefFrance.ServiceFranceClient proxyfr = new ServiceRefFrance.ServiceFranceClient();
        ServiceRefUK.ServiceUKClient proxyuk = new ServiceRefUK.ServiceUKClient();
        ServiceRefWorld.ServiceWorldClient proxyww = new ServiceRefWorld.ServiceWorldClient();



        [Authorize(Roles = "admin,user")]
        [ValidateInput(false)]
        public ActionResult Apaga()
        {
            MembershipUser u = Membership.GetUser(User.Identity.Name);
            if (u == null)
                return View("Error");
            else
                return View("Delete", u);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "admin,user")]
        [ValidateInput(false)]
        public ActionResult Apaga(string confirmButton)
        {
            try
            {
                //apagar todas as ofertas 
        

                    //delete de tudo relacionado com este utilizador

                    //get de todos os imoveis para este utilizador

                    var ptalladsbyidentidade = proxypt.GetAllAnunciosByIdentidade(User.Identity.Name);

                    foreach (var ptall in ptalladsbyidentidade.ToList())
                    {

                        try
                        {
            
                            proxypt.DeleteAnuncio(ptall.id);
                            proxypt.Save();                            
                        }
                        catch
                        {
                             return View("Error");
                        }

                    }



                    //apaga todas as empresas

                    var ptallemp=proxypt.GetAllEmpresaByID(User.Identity.Name);

                    foreach (var ptempall in ptallemp.ToList())
                    {
                        try
                        {

                            proxypt.DeleteEmpresa(ptempall.id);
                            proxypt.Save();
                        }
                        catch
                        {
                            return View("Error");
                        }


                    }
                   

                    
            }
            catch
            {
                return View("Error");

            }



            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////proxyde//////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                //apagar todas as ofertas 


                //delete de tudo relacionado com este utilizador

                //get de todos os imoveis para este utilizador

                var ptalladsbyidentidade = proxyde.GetAllAnunciosByIdentidade(User.Identity.Name);

                foreach (var ptall in ptalladsbyidentidade.ToList())
                {

                    try
                    {

                        proxyde.DeleteAnuncio(ptall.id);
                        proxyde.Save();
                    }
                    catch
                    {
                        return View("Error");
                    }

                }

                //apaga todas as empresas
                var ptallemp = proxyde.GetAllEmpresaByID(User.Identity.Name);


                foreach (var ptempall in ptallemp.ToList())
                {
                    try
                    {

                        proxyde.DeleteEmpresa(ptempall.id);
                        proxyde.Save();
                    }
                    catch
                    {
                        return View("Error");
                    }


                }



            }
            catch
            {
                return View("Error");

            }

            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////proxyfr//////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                //apagar todas as ofertas 


                //delete de tudo relacionado com este utilizador

                //get de todos os imoveis para este utilizador

                var ptalladsbyidentidade = proxyfr.GetAllAnunciosByIdentidade(User.Identity.Name);

                foreach (var ptall in ptalladsbyidentidade.ToList())
                {

                    try
                    {

                        proxyfr.DeleteAnuncio(ptall.id);
                        proxyfr.Save();
                    }
                    catch
                    {
                        return View("Error");
                    }

                }

                //apaga todas as empresas

                var ptallemp = proxyfr.GetAllEmpresaByID(User.Identity.Name);

                foreach (var ptempall in ptallemp.ToList())
                {
                    try
                    {

                        proxyfr.DeleteEmpresa(ptempall.id);
                        proxyfr.Save();
                    }
                    catch
                    {
                        return View("Error");
                    }


                }



            }
            catch
            {
                return View("Error");

            }





            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////proxyes//////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                //apagar todas as ofertas 


                //delete de tudo relacionado com este utilizador

                //get de todos os imoveis para este utilizador

                var ptalladsbyidentidade = proxyes.GetAllAnunciosByIdentidade(User.Identity.Name);

                foreach (var ptall in ptalladsbyidentidade.ToList())
                {

                    try
                    {

                        proxyes.DeleteAnuncio(ptall.id);
                        proxyes.Save();
                    }
                    catch
                    {
                        return View("Error");
                    }

                }

                //apaga todas as empresas
                var ptallemp = proxyes.GetAllEmpresaByID(User.Identity.Name);

                foreach (var ptempall in ptallemp.ToList())
                {
                    try
                    {

                        proxyes.DeleteEmpresa(ptempall.id);
                        proxyes.Save();
                    }
                    catch
                    {
                        return View("Error");
                    }


                }



            }
            catch
            {
                return View("Error");

            }




            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////proxyuk//////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                //apagar todas as ofertas 


                //delete de tudo relacionado com este utilizador

                //get de todos os imoveis para este utilizador

                var ptalladsbyidentidade = proxyuk.GetAllAnunciosByIdentidade(User.Identity.Name);

                foreach (var ptall in ptalladsbyidentidade.ToList())
                {

                    try
                    {

                        proxyuk.DeleteAnuncio(ptall.id);
                        proxyuk.Save();
                    }
                    catch
                    {
                        return View("Error");
                    }

                }

                //apaga todas as empresas
                var ptallemp = proxyuk.GetAllEmpresaByID(User.Identity.Name);

                foreach (var ptempall in ptallemp.ToList())
                {
                    try
                    {

                        proxyuk.DeleteEmpresa(ptempall.id);
                        proxyuk.Save();
                    }
                    catch
                    {
                        return View("Error");
                    }


                }



            }
            catch
            {
                return View("Error");

            }

            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////proxyww//////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                //apagar todas as ofertas 


                //delete de tudo relacionado com este utilizador

                //get de todos os imoveis para este utilizador

                var ptalladsbyidentidade = proxyww.GetAllAnunciosByIdentidade(User.Identity.Name);

                foreach (var ptall in ptalladsbyidentidade.ToList())
                {

                    try
                    {

                        proxyww.DeleteAnuncio(ptall.id);
                        proxyww.Save();
                    }
                    catch
                    {
                        return View("Error");
                    }

                }

                //apaga todas as empresas
                var ptallemp = proxyww.GetAllEmpresaByID(User.Identity.Name);

                foreach (var ptempall in ptallemp.ToList())
                {
                    try
                    {

                        proxyww.DeleteEmpresa(ptempall.id);
                        proxyww.Save();
                    }
                    catch
                    {
                        return View("Error");
                    }


                }


            }
            catch
            {
                return View("Error");

            }




            // Excluindo Usuário
            if (Membership.DeleteUser(User.Identity.Name))
            {
                //Session.Abandon();

                return RedirectToAction("LogOff", "Account");

                //return View("Deleted");
            }
            else
            {
                return View("Error");
            }


            //return View("Deleted");


        }

        //
        // GET: /ApagaConta/

        public ActionResult Index()
        {
            return View();
        }

    }
}
