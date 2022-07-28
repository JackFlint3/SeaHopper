using System.Collections;

namespace SeaHopper.Compiler.Lexer
{
    public class FileIterator : IEnumerator<char>
    {
        int pos;
        string source;
        private bool eof = false;


        public FileIterator( string source)
        {
            pos = 0;
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public char Current => source[pos];

        public bool EOF { get => eof; }

        object IEnumerator.Current => source[pos];

        public void Dispose()
        {
            return;
        }

        public bool MoveNext()
        {
            pos++;
            eof = !(source.Length > pos);
            return !eof;
        }

        public void Reset()
        {
            eof = false;
            pos = 0;
        }
    }   
}
