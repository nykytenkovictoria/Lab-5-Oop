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
            set { 
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
            Console.WriteLine("[Document] Статичний конструктор: клас ініціалізовано.");
        }

        public Document()
        {
            _docId = "D-000";
            _docType = "—";
            CreatedBy = "—";
            _signed = false;
            _archived = false;
            FullInfo = BuildInfo();
            _totalDocs++;
            Console.WriteLine("[Document] Конструктор без параметрів: документ створено.");
        }

        public Document(string docId, string docType)
        {
            _docId = docId;
            _docType = docType;
            CreatedBy = "System";
            _signed = false;
            _archived = false;
            FullInfo = BuildInfo();
            _totalDocs++;
            Console.WriteLine($"[Document] Конструктор з параметрами: [{_docId}] '{_docType}'");
        }

        public Document(string docId, string docType, string createdBy)
            : this(docId, docType)
        {
            CreatedBy = createdBy;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Document] Конструктор виклику іншого: автор={CreatedBy}");
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
            Console.WriteLine($"[Document] Конструктор копії: скопійовано [{_docId}]");
        }

        private Document(string docId)
        {
            _docId = docId;
            _docType = "Шаблон";
            CreatedBy = "System";
            FullInfo = BuildInfo();
            Console.WriteLine($"[Document] Закритий конструктор: шаблон [{_docId}]");
        }

        public static Document CreateTemplate(string id) => new Document(id);

        private string BuildInfo() =>
            $"[{_docId}] {_docType} | Автор: {CreatedBy} | Підписано: {_signed} | Архів: {_archived}";

        // Creates the document in the electronic workflow.
        public void Create()
        {
            Console.WriteLine($"[Document] Created: [{_docId}] type='{_docType}'");
        }

        // Signs the document digitally.
        public void Sign(string signerName)
        {
            _signed = true;
            Console.WriteLine($"[Document] [{_docId}] signed by '{signerName}'");
        }

        /// Archives the document.
        public void Archive()
        {
            _archived = true;
            Console.WriteLine($"[Document] [{_docId}] archived. (Signed: {_signed})");
        }


        public void PrintInfo()
        {
            Console.WriteLine($"  ID           : {_docId}");
            Console.WriteLine($"  Тип          : {_docType}");
            Console.WriteLine($"  Автор        : {CreatedBy}");
            Console.WriteLine($"  Підписано    : {_signed}");
            Console.WriteLine($"  Архівовано   : {_archived}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
            Console.WriteLine($"  Всього докум.: {TotalDocs}");
        }

        public override string ToString() => $"Document [{_docId}] '{_docType}' | signed={_signed}";
    }
}