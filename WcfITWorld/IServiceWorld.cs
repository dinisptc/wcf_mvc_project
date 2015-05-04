using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfITPortugal.Viewmodels;
using System.IO;

namespace WcfITWorld
{

    [MessageContract]
    public class StreamData
    {
        [MessageHeader]
        public string Codigo;

        [MessageBodyMember]
        public Stream Conteudo;
    }


    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceWorld
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        int AddAnuncioModel(AnuncioModel anuncio);

        [OperationContract]
        bool AddDescricao(DescricaoModel desc);

        [OperationContract]
        int GetIdioma_ID(string idiomacode);

        [OperationContract]
        List<empresa> GetAllEmpresaByID(string identidade);

        [OperationContract]
        List<PortugalViewModel> GetAllAnunciosID(string idiomacode);

        [OperationContract]
        void UpdateVisitasAnuncioPortugal(int id);

        [OperationContract]
        anuncio GetAnunciosProcuraIndexByID(int id);

        [OperationContract]
        anunciodesc GetAnunciosDescProcuraIndexByIDAndLangCode(int id, string lang);

        [OperationContract]
        int GetCountIdiomas(int id);

        [OperationContract]
        List<anunciodesc> GetAnunciosDescProcuraIndexByIDALL(int id);

        [OperationContract]
        //List<PortugalViewModel> GetAnunciosIndexByIdentidade(string user);
        List<PortugalViewModel> GetAnunciosIndexByIdentidade(string user, string idiomacode);
        [OperationContract]
        empresa GetAllEmpresaByEmpresaID(int empresa_id);

        [OperationContract]
        int AddEmpresa(EmpresaModel desc);

        [OperationContract]
        void Upload(StreamData file);

        [OperationContract]
        void UpdateImage_file(int id, string file_name_fto);

        [OperationContract]
        List<empresa> GetEmpresasIndexByIdentidade(string user);


        [OperationContract]
        void UpdateEmpresa(int id, EmpresaModel desc);

        [OperationContract]
        empresa GetEmpresaIndexByID(int id);

        [OperationContract]
        void DeleteEmpresa(int id);

        [OperationContract]
        void DeleteAnuncio(int id);

        [OperationContract]
        void Save();

        [OperationContract]
        List<empresa> GetEmpresasTodasasEmpresas();

        [OperationContract]
        //void UpdateAnuncio(int id, AnuncioModel procura, string lang);
        void UpdateAnuncio(int id, AnuncioModel procura, DescricaoModel desc, string lang);

        [OperationContract]
        void UpdateAnuncioDescricao(int id, DescricaoModel desc, string lang);

        [OperationContract]
        List<SearchPortugalViewModel> Search(SearchPortugalViewModel collection, int idiomacode);

        [OperationContract]
        List<anuncio> GetAllAnuncios();

        [OperationContract]
        List<anuncio> GetAllAnunciosByIdentidade(string user);
        // TODO: Add your service operations here


    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    #region procuramodel
    [DataContract]
    public class AnuncioModel
    {

        DateTime dataanuncio = DateTime.Now;
        int id = 0;
        int numvisitas = 0;
        string email = "";
        string local_de_trabalho = "";
        string salario = "";
        int empresa_id = 0;
        string identidade = "";

        //string titulo = "";
        //string descricao="";

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int Numvisitas
        {
            get { return numvisitas; }
            set { numvisitas = value; }
        }

        [DataMember]
        public int EmpresaId
        {
            get { return empresa_id; }
            set { empresa_id = value; }
        }

        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DataMember]
        public string Local_de_trabalho
        {
            get { return local_de_trabalho; }
            set { local_de_trabalho = value; }
        }

        [DataMember]
        public string Salario
        {
            get { return salario; }
            set { salario = value; }
        }

        [DataMember]
        public DateTime Dataanuncio
        {
            get { return dataanuncio; }
            set { dataanuncio = value; }
        }

        [DataMember]
        public string Identidade
        {
            get { return identidade; }
            set { identidade = value; }
        }

    }

    #endregion

    [DataContract]
    public class DescricaoModel
    {
        string descricao = "";
        string titulo = "";
        int idioma_id = 0;
        int anuncio_id = 0;

        [DataMember]
        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        [DataMember]
        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        [DataMember]
        public int Idioma_id
        {
            get { return idioma_id; }
            set { idioma_id = value; }
        }

        [DataMember]
        public int Anuncio_id
        {
            get { return anuncio_id; }
            set { anuncio_id = value; }
        }
    }

    [DataContract]
    public class EmpresaModel
    {
        string nome_da_empresa = "";
        string url = "";
        string logo = "";
        string contacto = "";
        string username = "";
        int id = 0;


        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string Contacto
        {
            get { return contacto; }
            set { contacto = value; }
        }

        [DataMember]
        public string Logo
        {
            get { return logo; }
            set { logo = value; }
        }

        [DataMember]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        [DataMember]
        public string Nome_da_empresa
        {
            get { return nome_da_empresa; }
            set { nome_da_empresa = value; }
        }

    }
}