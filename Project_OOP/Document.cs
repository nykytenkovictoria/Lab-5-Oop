// Document.cs
// Електронний документообіг університету

namespace DigitalUniversity
{
    public class Document
    {
        private string _docId;
        private string _docType;
        private bool _signed;
        private bool _archived;
        private static int _totalDocs;

        public string DocId
        {
            get => _docId;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _docId = value;
            }
        }

        public string DocType
        {
            get => _docType;
            set => _docType = value;
        }

        public bool IsSigned => _signed;
        public bool IsArchived => _archived;

        public static int TotalDocs => _totalDocs;

        public string CreatedBy { get; set; }

        public string FullInfo { get; private set; }

        static Document()
        {
            _totalDocs = 0;
        }

        public Document()
        {
            _docId = Messages.Get("document", "default_id");
            _docType = Messages.Get("document", "default_type");
            CreatedBy = Messages.Get("document", "default_created_by");
            _signed = false;
            _archived = false;
            FullInfo = BuildInfo();
            _totalDocs++;
        }

        public Document(string docId, string docType)
        {
            _docId = docId;
            _docType = docType;
            CreatedBy = Messages.Get("document", "system_created_by");
            _signed = false;
            _archived = false;
            FullInfo = BuildInfo();
            _totalDocs++;
        }

        public Document(string docId, string docType, string createdBy)
            : this(docId, docType)
        {
            CreatedBy = createdBy;
            FullInfo = BuildInfo();
        }

        public Document(Document other)
        {
            _docId = other._docId + "-copy";
            _docType = other._docType;
            CreatedBy = other.CreatedBy;
            _signed = false;
            _archived = false;
            FullInfo = BuildInfo();
            _totalDocs++;
        }

        private Document(string docId)
        {
            _docId = docId;
            _docType = Messages.Get("document", "template_type");
            CreatedBy = Messages.Get("document", "system_created_by");
            FullInfo = BuildInfo();
        }

        public static Document CreateTemplate(string id) => new Document(id);

        public bool IsReadyToIssue() => _signed && !_archived;

        /// Чи документ потребує підпису
        public bool NeedsSignature() => !_signed && !_archived;

        /// Чи документ в архіві
        public bool IsInArchive() => _archived;

        private string BuildInfo() =>
            Messages.Get("document", "build_info", _docId, _docType, CreatedBy, _signed, _archived);

        // Creates the document in the electronic workflow.
        public void Create()
        {
            Messages.Print("document", "create", _docId, _docType);
        }

        // Signs the document digitally.
        public void Sign(string signerName)
        {
            if (_archived)
            {
                Messages.Print("document", "sign_archived", _docId);
                return;
            }
            _signed = true;
            FullInfo = BuildInfo();
            Messages.Print("document", "sign_success", _docId, signerName);
        }


        /// Archives the document.
        public void Archive()
        {
            if (!_signed)
                Messages.Print("document", "archive_unsigned_warning", _docId);
            _archived = true;
            FullInfo = BuildInfo();
            Messages.Print("document", "archive_success", _docId);
        }


        public void PrintInfo()
        {
            Console.WriteLine($"  {Messages.Get("document", "id_label")}           : {_docId}");
            Console.WriteLine($"  {Messages.Get("document", "type_label")}          : {_docType}");
            Console.WriteLine($"  {Messages.Get("document", "author_label")}        : {CreatedBy}");
            Console.WriteLine($"  {Messages.Get("document", "signed_label")}    : {_signed}");
            Console.WriteLine($"  {Messages.Get("document", "archived_label")}   : {_archived}");
            Console.WriteLine($"  {Messages.Get("document", "ready_label")}      : {IsReadyToIssue()}");
            Console.WriteLine($"  {Messages.Get("document", "fullinfo_label")}     : {FullInfo}");
            Console.WriteLine($"  {Messages.Get("document", "total_docs_label")}: {TotalDocs}");
        }

        public override string ToString() => Messages.Get("document", "to_string", _docId, _docType, _signed);
    }
}