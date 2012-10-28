using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Scripting {
    public static class RegistrationCodes {
        public static string Synthesize(string container, string name, params string[] arguments) {
            var commaSepArgs = arguments.Any() ? arguments.Aggregate((a, c) => a + "," + c) : string.Empty;
            var code = string.Format(
                "if(!{0}) {{" +
                "    var {0} = {{ }};" +
                "}}" +
                "{0}.{1} = function({2}) {{" +
                "    native function __{1}();" +
                "    return __{1}({2});" +
                "}}", container, name, commaSepArgs);
            return code;
        }
    }
}
