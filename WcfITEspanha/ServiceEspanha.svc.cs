using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfITPortugal.Viewmodels;
using WcfServiceBomQuarto.Libs;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using MvcTIC_IT.Libraries;

namespace WcfITEspanha
{



    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class ServiceEspanha : IServiceEspanha
    {

        EspanhaEntities db = new EspanhaEntities();

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int AddAnuncioModel(AnuncioModel procura)
        {

            anuncio anuncio = new anuncio();

            anuncio.dataanuncio = procura.Dataanuncio;
            anuncio.email = procura.Email;
            anuncio.identidade = procura.Identidade;
            anuncio.numvisitas = procura.Numvisitas;
            anuncio.salario = procura.Salario;
            anuncio.local_de_trabalho = procura.Local_de_trabalho;
            anuncio.empresa_id = procura.EmpresaId;

            try
            {
                db.AddToanuncio(anuncio);
                db.SaveChanges();

                return anuncio.id;
            }
            catch
            {
                return -1;
            }


        }

        /***********************************Update anuncio*********************************************/
        public void UpdateAnuncio(int id, AnuncioModel procura, DescricaoModel desc, string lang)
        {

            var proc = (from p in db.anuncio
                        where (p.id == id)
                        select p).SingleOrDefault();

            proc.dataanuncio = procura.Dataanuncio;
            proc.email = procura.Email;
            //proc.identidade = procura.Identidade;
            //proc.numvisitas = procura.Numvisitas;
            proc.salario = procura.Salario;
            proc.local_de_trabalho = procura.Local_de_trabalho;
            proc.empresa_id = procura.EmpresaId;

            var procdesc = (from p in db.anunciodesc
                            where ((p.anuncio_id == id) && (p.idiomas.lang_code == lang))
                            select p).SingleOrDefault();



            if (procdesc == null)
            {

            }
            else
            {
                //procdesc.idioma_id = procd.Idioma_id;
                //procdesc.anuncio_id = procd.Anuncio_id;
                procdesc.descricao = desc.Descricao;
                procdesc.titulo = desc.Titulo;
            }


            try
            {

                db.SaveChanges();

                //return proc.id;
            }
            catch
            {
                //return -1;
            }
        }
        /***********************************Update anuncio descricao*********************************************/
        public void UpdateAnuncioDescricao(int id, DescricaoModel desc, string lang)
        {





            var procdesc = (from p in db.anunciodesc
                            where ((p.anuncio_id == id) && (p.idiomas.lang_code == lang))
                            select p).SingleOrDefault();


            procdesc.descricao = desc.Descricao;
            procdesc.titulo = desc.Titulo;



            try
            {

                db.SaveChanges();


            }
            catch
            {

            }
        }
        public bool AddDescricao(DescricaoModel desc)
        {

            anunciodesc proc = new anunciodesc();

            proc.idioma_id = desc.Idioma_id;
            proc.anuncio_id = desc.Anuncio_id;
            proc.descricao = desc.Descricao;
            proc.titulo = desc.Titulo;

            try
            {
                db.AddToanunciodesc(proc);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }

        ///buscar os idiomas
        //ver qual eh o actual
        //carregar as categorias com o idioma activo no momento
        public int GetIdioma_ID(string idiomacode)
        {
            return (from c in db.idiomas
                    where c.lang_code == idiomacode
                    select c.id).SingleOrDefault();

        }


        public List<empresa> GetAllEmpresaByID(string identidade)
        {


            var ubu = (from c in db.empresa
                       where c.username == identidade
                       select c);


            return ubu.ToList();
        }


        public empresa GetAllEmpresaByEmpresaID(int empresa_id)
        {
            var ubu = (from c in db.empresa
                       where c.id == empresa_id
                       select c).SingleOrDefault();
            return ubu;
        }

        public List<PortugalViewModel> GetAnunciosIndexByIdentidade(string user, string idiomacode)
        {

            var unu = (from imov in db.anuncio
                       where (imov.identidade == user)
                       select new PortugalViewModel
                       {
                           dataanuncio = imov.dataanuncio,
                           email = imov.email,
                           /*by identidade*/
                           identidade = imov.identidade,
                           numvisitas = (int)imov.numvisitas,
                           salario = imov.salario,
                           local_de_trabalho = imov.local_de_trabalho,
                           empresa_id = imov.empresa_id,
                           id = imov.id,

                           titulo = (from imodesc in db.anunciodesc
                                     where (imodesc.anuncio_id == imov.id) && (imodesc.idiomas.lang_code == idiomacode)
                                     select imodesc.titulo).FirstOrDefault(),

                           descricao = (from imodesc in db.anunciodesc
                                        where (imodesc.anuncio_id == imov.id) && (imodesc.idiomas.lang_code == idiomacode)
                                        select imodesc.descricao).FirstOrDefault()
                       }).OrderByDescending(d => d.dataanuncio);

            return unu.ToList();

        }

        public List<anuncio> GetAllAnuncios()
        {
            var unu = (from imov in db.anuncio
                       select imov).OrderByDescending(d => d.dataanuncio);


            return unu.ToList();
        }


        public List<anuncio> GetAllAnunciosByIdentidade(string user)
        {
            var unu = (from imov in db.anuncio
                       where (imov.identidade == user)
                       select imov).OrderByDescending(d => d.dataanuncio);


            return unu.ToList();
        }

        public List<PortugalViewModel> GetAllAnunciosID(string idiomacode)
        {


            var unu = (from imov in db.anuncio
                       select new PortugalViewModel
                       {
                           dataanuncio = imov.dataanuncio,
                           email = imov.email,
                           identidade = imov.identidade,
                           numvisitas = (int)imov.numvisitas,
                           salario = imov.salario,
                           local_de_trabalho = imov.local_de_trabalho,
                           empresa_id = imov.empresa_id,
                           id = imov.id,

                           titulo = (from imodesc in db.anunciodesc
                                     where (imodesc.anuncio_id == imov.id) && (imodesc.idiomas.lang_code == idiomacode)
                                     select imodesc.titulo).FirstOrDefault(),

                           descricao = (from imodesc in db.anunciodesc
                                        where (imodesc.anuncio_id == imov.id) && (imodesc.idiomas.lang_code == idiomacode)
                                        select imodesc.descricao).FirstOrDefault()
                       }).OrderByDescending(d => d.dataanuncio);

            return unu.ToList();


        }


        public void UpdateVisitasAnuncioPortugal(int id)
        {
            var imoi = (from p in db.anuncio
                        where p.id == id
                        select p).SingleOrDefault();

            imoi.numvisitas = imoi.numvisitas + 1;

            db.SaveChanges();
        }

        /*****************-------------GetAnunciosProcuraIndexByID----------------------***********************/
        public anuncio GetAnunciosProcuraIndexByID(int id)
        {
            var model = (from c in db.anuncio
                         where (c.id == id)
                         select c).SingleOrDefault();
            return model;
        }

        /*****************-------------GetAnunciosDescProcuraIndexByIDAndLangCode----------------------***********************/
        public anunciodesc GetAnunciosDescProcuraIndexByIDAndLangCode(int id, string lang)
        {

            int langcode = GetIdioma_ID(lang);
            var model = (from c in db.anunciodesc
                         where (c.anuncio_id == id) && (c.idioma_id == langcode)
                         select c).SingleOrDefault();
            return model;

        }


        /*****************-------------GetCountIdiomas----------------------***********************/
        public int GetCountIdiomas(int id)
        {
            return (from c in db.anunciodesc
                    where (c.anuncio_id == id)
                    select c).Count();
        }


        /*****************-------------GetAnunciosDescProcuraIndexByIDALL----------------------***********************/
        public List<anunciodesc> GetAnunciosDescProcuraIndexByIDALL(int id)
        {
            var model = (from c in db.anunciodesc
                         where (c.anuncio_id == id)
                         select c);
            return model.ToList();
        }


        /*****************-------------DeleteAnuncio----------------------***********************/
        public void DeleteAnuncio(int id)
        {

            anuncio proc = db.anuncio.SingleOrDefault(d => d.id == id);

            //apagar descricoes de imovel
            //in imovel_id
            DeleteDescricoes(proc.id);

            db.anuncio.DeleteObject(proc);
            db.SaveChanges();
        }


        public void DeleteDescricoes(int id)
        {

            var imos = (from c in db.anunciodesc
                        where c.anuncio_id == id
                        select c);

            foreach (anunciodesc imod in imos.ToList())
            {
                db.anunciodesc.DeleteObject(imod);
                db.SaveChanges();
            }

        }


        public void Save()
        {
            db.SaveChanges();
        }
        /*****************-------------logo----------------------***********************/
        ApagarFotosNoDisco apagafoto = new ApagarFotosNoDisco();

        /*****************-------------empresa----------------------***********************/

        public List<empresa> GetEmpresasTodasasEmpresas()
        {

            var model = (from c in db.empresa
                         select c);


            return model.ToList();

        }


        public List<empresa> GetEmpresasIndexByIdentidade(string user)
        {

            var model = (from c in db.empresa
                         where (c.username == user)
                         select c);


            return model.ToList();

        }


        public int AddEmpresa(EmpresaModel desc)
        {

            empresa proc = new empresa();

            proc.logo = desc.Logo;
            proc.nome_da_empresa = desc.Nome_da_empresa;
            proc.url = desc.Url;
            proc.username = desc.Username;
            proc.contacto = desc.Contacto;

            try
            {
                db.AddToempresa(proc);
                db.SaveChanges();

                return proc.id;
            }
            catch
            {
                return -1;
            }


            //try
            //{
            //    db.AddToempresa(proc);
            //    db.SaveChanges();
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}

        }

        public void Upload(StreamData file)
        {


            settings set = new settings();
            string imagesDirt = set.logo;


            ResizeStream(300, file.Conteudo, Path.Combine(imagesDirt, file.Codigo));
        }

        public void UpdateImage_file(int id, string file_name_fto)
        {


            settings set = new settings();
            string imagesDirt = set.logo;

            var cati = (from p in db.empresa
                        where p.id == id
                        select p).Single();

            cati.logo = file_name_fto;


            db.SaveChanges();



        }

        /***********************************Update empresa1*********************************************/
        public void UpdateEmpresa(int id, EmpresaModel desc)
        {

            var proc = (from p in db.empresa
                        where (p.id == id)
                        select p).SingleOrDefault();

            //proc.logo = desc.Logo;
            proc.nome_da_empresa = desc.Nome_da_empresa;
            proc.url = desc.Url;
            proc.username = desc.Username;
            proc.contacto = desc.Contacto;

            try
            {

                db.SaveChanges();

                //return proc.id;
            }
            catch
            {
                //return -1;
            }
        }

        /*************************GETEMPRESAID*******************************/

        public empresa GetEmpresaIndexByID(int id)
        {
            var model = (from c in db.empresa
                         where (c.id == id)
                         select c).SingleOrDefault();
            return model;
        }

        /****************************DELETEEMPRESA****************************/
        public void DeleteEmpresa(int id)
        {

            //apagar logo

            empresa empresa = db.empresa.Single(e => e.id == id);

            settings set = new settings();
            string imagesDir = set.logo;



            if (id == 19)
            {
                //do not delete anonymous
            }
            else
            {
                if (empresa.logo.Equals("nologo.jpg"))
                {

                }
                else
                {
                    string result1 = Path.Combine(imagesDir, empresa.logo);
                    apagafoto.DeleteFile(result1);
                }

                db.empresa.DeleteObject(empresa);
                db.SaveChanges();
            }

        }

        /************ResizeStream******************************************************/
        public void ResizeStream(int imageSize, Stream filePath, string outputPath)
        {
            var image = System.Drawing.Image.FromStream(filePath);

            int thumbnailSize = imageSize;
            int newWidth, newHeight;

            if (image.Width > image.Height)
            {
                newWidth = thumbnailSize;
                newHeight = image.Height * thumbnailSize / image.Width;
            }
            else
            {
                newWidth = image.Width * thumbnailSize / image.Height;
                newHeight = thumbnailSize;
            }

            var thumbnailBitmap = new Bitmap(newWidth, newHeight);

            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(image, imageRectangle);

            thumbnailBitmap.Save(outputPath, image.RawFormat);
            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();
        }



        public List<SearchPortugalViewModel> Search(SearchPortugalViewModel collection, int idiomacode)
        {
            var q = (from imov in db.anuncio
                     join o in db.anunciodesc on imov.id equals o.anuncio_id
                     where (
                      (imov.salario.Contains(collection.salario) || collection.salario == null) &&
                      (imov.local_de_trabalho.Contains(collection.local_de_trabalho) || collection.local_de_trabalho == null) &&
                      (o.titulo.Contains(collection.titulo) || collection.titulo == null) &&
                      (o.descricao.Contains(collection.descricao) || collection.descricao == null))
                     select new SearchPortugalViewModel
                     {
                         salario = imov.salario,
                         local_de_trabalho = imov.local_de_trabalho,
                         titulo = (from imodesc in db.anunciodesc
                                   where (imodesc.anuncio_id == imov.id) && (imodesc.idioma_id == idiomacode)
                                   select imodesc.titulo).FirstOrDefault(),

                         descricao = (from imodesc in db.anunciodesc
                                      where (imodesc.anuncio_id == imov.id) && (imodesc.idioma_id == idiomacode)
                                      select imodesc.descricao).FirstOrDefault(),
                         empresa_id = imov.empresa_id,
                         id = imov.id,
                         dataanuncio = imov.dataanuncio,
                         identidade = imov.identidade,
                         numvisitas = (int)imov.numvisitas,
                         email = imov.email,
                     }).Distinct().OrderByDescending(d => d.dataanuncio); ;

            return q.ToList();
        }
    }
}
