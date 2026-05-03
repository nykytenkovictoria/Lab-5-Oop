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

        public string DocId => _docId;
        public string DocType => _docType;

        public Document(string docId, string docType)
        {
            _docId = docId;
            _docType = docType;
            _signed = false;
            _archived = false;
        }

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

        public override string ToString() => $"Document [{_docId}] '{_docType}' | signed={_signed}";
    }
}