using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Nwazet.ZenGallery.Models;
using Nwazet.ZenGallery.Settings;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.MediaLibrary.Models;
using Orchard.MediaLibrary.Services;
using Orchard.Tokens;

namespace Nwazet.ZenGallery.Services {
    public interface IZenGalleryService : IDependency {
        string GeneratePath(ZenGalleryPart part);
        string GetDefaultPattern(string contentType);
        bool IsPathValid(string path);
        IEnumerable<MediaPart> GetImages(ZenGalleryPart part);
    }

    [OrchardFeature("Nwazet.ZenGallery")]
    public class ZenGalleryService : IZenGalleryService {
        private readonly ITokenizer _tokenizer;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IMediaLibraryService _mediaLibraryService;

        public ZenGalleryService(
            ITokenizer tokenizer,
            IContentDefinitionManager contentDefinitionManager,
            IMediaLibraryService mediaService) {

            _tokenizer = tokenizer;
            _contentDefinitionManager = contentDefinitionManager;
            _mediaLibraryService = mediaService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public string GeneratePath(ZenGalleryPart part) {

            if (part == null) {
                throw new ArgumentNullException("part");
            }

            var pattern = GetDefaultPattern(part.ContentItem.ContentType);
            // if the content type has no pattern for the path, then use a default one
            if (string.IsNullOrWhiteSpace(pattern)) {
                pattern = "gallery/{Content.Slug}";
            }

            // Convert the pattern via tokens
            var path = _tokenizer.Replace(pattern, BuildTokenContext(part.ContentItem), new ReplaceOptions { Encoding = ReplaceOptions.NoEncode });

            // removing trailing slashes in case the container is empty, and tokens are base on it (e.g. home page)
            while(path[0] == '/') {
                path = path.Substring(1);
            }

            return path;
        }

        private static IDictionary<string, object> BuildTokenContext(IContent item) {
            return new Dictionary<string, object> { { "Content", item } };
        }

        public string GetDefaultPattern(string contentType) {
            var settings = GetTypePartSettings(contentType).GetModel<ZenGalleryPartSettings>();
            return settings.PathPattern;
        }

        private SettingsDictionary GetTypePartSettings(string contentType) {
            var contentDefinition = _contentDefinitionManager.GetTypeDefinition(contentType);
            
            if (contentDefinition == null) {
                throw new OrchardException(T("Unknown content type: {0}", contentType));
            }

            return contentDefinition.Parts.First(x => x.PartDefinition.Name == "ZenGalleryPart").Settings;
        }

        public bool IsPathValid(string path) {
            return String.IsNullOrWhiteSpace(path) || Regex.IsMatch(path, @"^[^:?#\[\]@!$&'()*+,;=\s\""\<\>\\]+$") && !(path.StartsWith(".") || path.EndsWith("."));
        }

        public IEnumerable<MediaPart> GetImages(ZenGalleryPart part) {
            ////if (part == null || String.IsNullOrWhiteSpace(part.Path)) return new MediaPart[0];
            ////var lastSlash = part.Path.LastIndexOf('/');
            ////if (lastSlash == -1 || lastSlash >= part.Path.Length - 2) return new MediaPart[0];
            ////var parent = part.Path.Substring(0, lastSlash);
            ////var folder = part.Path.Substring(lastSlash + 1);
            ////if (String.IsNullOrWhiteSpace(folder)) return new MediaPart[0];
            ////if (_mediaLibraryService.GetMediaFolders(parent).All(f => f.Name != folder)) {
            ////    _mediaLibraryService.CreateFolder(parent, folder);
            ////}
            ////var path = part.Path.Replace("/", "\\");

            var path = part.Path;

            return _mediaLibraryService
                .GetMediaContentItems(path, 0, int.MaxValue, null, null)
                .OrderBy(p => p.FileName)
                .ToList();
        }
    }
}