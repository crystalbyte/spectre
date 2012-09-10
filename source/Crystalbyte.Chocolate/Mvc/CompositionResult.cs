using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Mvc
{
    public sealed class CompositionResult {
        private readonly string _markup;
        private readonly IEnumerable<Exception> _errors;
        public CompositionResult(string markup, IEnumerable<Exception> errors = null) {
            _markup = markup;
            _errors = errors;
        }

        public IEnumerable<Exception> Errors
        {
            get { return _errors; }
        }

        public string Markup
        {
            get { return _markup; }
        }

        public bool IsErrornous {
            get { return _errors != null && !_errors.Any(); }
        }
    }
}
