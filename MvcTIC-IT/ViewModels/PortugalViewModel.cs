using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MvcTIC_IT.ServiceRefPortugal;
using PagedList;

namespace MvcTIC_IT.ViewModels
{
    public class PortugalViewModel
    {

        public int? Page { get; set; }

        public IPagedList<PortugalViewModel> SearchResults { get; set; }
        //public IPagedList<anuncio> SearchResults { get; set; }


        public int? id { get; set; }

        [Required(ErrorMessageResourceName = "email_required", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceName = "email", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessageResourceName = "local_de_trabalho_required", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        public string local_de_trabalho { get; set; }

        [Required(ErrorMessageResourceName = "salario_required", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        public string salario { get; set; }

           [Required(ErrorMessageResourceName = "empresa_required", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        public int empresa_id { get; set; }

        [StringLength(200, ErrorMessageResourceName = "titulo200", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        [Required(ErrorMessageResourceName = "titulorequired", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        public string titulo { get; set; }

        [StringLength(5000, ErrorMessageResourceName = "descricaoover4000", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        [Required(ErrorMessageResourceName = "descricaorequired", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }

        public string identidade { get; set; }

        public int idioma_id { get; set; }

        public int anuncio_id { get; set; }

        public int numvisitas { get; set; }

        public DateTime dataanuncio { get; set; }
    }
}