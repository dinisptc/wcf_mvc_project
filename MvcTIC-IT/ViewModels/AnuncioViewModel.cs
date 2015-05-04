using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcTIC_IT.ViewModels
{
    public class AnuncioViewModel
    {

        [Required(ErrorMessageResourceName = "email_required", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceName = "email", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        public string local_de_trabalho { get; set; }

        public string salario { get; set; }

        public int empresa_id { get; set; }

        public string titulo { get; set; }

        public string descricao { get; set; }

        public int idioma_id { get; set; }

        public int anuncio_id { get; set; }

        public DateTime data_criacao { get; set; }
    }
}