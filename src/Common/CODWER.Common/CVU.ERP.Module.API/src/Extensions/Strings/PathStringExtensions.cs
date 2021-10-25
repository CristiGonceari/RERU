using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CVU.ERP.Module.API.Extensions.Strings {
    public static class PathStringExtensions {
        public static bool StartsWithAnySegments (this PathString path, string[] paths) {
            return paths.Any (p => path.StartsWithSegments (new PathString (p)));
        }
    }
}