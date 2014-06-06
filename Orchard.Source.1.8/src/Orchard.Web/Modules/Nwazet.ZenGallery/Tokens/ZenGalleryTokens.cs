using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nwazet.ZenGallery.Models;
using Nwazet.ZenGallery.Services;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Tokens;

namespace Nwazet.ZenGallery.Tokens {
    public class ZenGalleryTokens : ITokenProvider {
        private readonly IZenGalleryService _galleryService;

        public ZenGalleryTokens(IZenGalleryService galleryService) {
            _galleryService = galleryService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context) {
            context.For("Content", T("Content Items"), T("Content Items"))
                .Token("ZenGallery", T("Zen Gallery"), T("The collection of Zen Gallery image URLs."), "ZenGallery")
                .Token("ZenGallery:*", T("Zen Gallery image"), T("An image in the Zen Gallery by index."), "Url");
            context.For("ZenGallery", T("Zen Gallery"), T("List of images in a Zen Gallery."))
                .Token("First", T("First Image"), T("The first image in the list."), "Url")
                .Token("Last", T("Last Image"), T("The first image in the list."))
                .Token("Concat:*", T("Concat:<format>"), T("The concatenation of all image URLs in the Zen Gallery, using the provided format where {{0}} represents the image URL, and an optional separator specified after a comma. For example, <img src=\"{{0}}\"/>, | will put give the img rendering for all images, concatenated and with pipe characters between them."));
        }

        public void Evaluate(EvaluateContext context) {
            context.For<IContent>("Content")
                .Token("ZenGallery", content => String.Join(",", _galleryService.GetImages(content.As<ZenGalleryPart>())))
                .Chain("ZenGallery", "ZenGallery", content => _galleryService.GetImages(content.As<ZenGalleryPart>()))
                .Token(token => token.StartsWith("ZenGallery:") ? token.Substring("ZenGallery:".Length) : null,
                    (index, gallery) => _galleryService.GetImages(gallery.As<ZenGalleryPart>()).ElementAt(int.Parse(index))
                );

            context.For<IEnumerable<string>>("ZenGallery")
                .Token(
                    token => token == String.Empty ? String.Empty : null,
                    (token, gallery) => String.Join(",", gallery))
                .Token("First", gallery => gallery.FirstOrDefault() ?? "")
                .Chain("First", "Url", gallery => gallery.FirstOrDefault() ?? "")
                .Token("Last", gallery => gallery.LastOrDefault() ?? "")
                .Chain("Last", "Url", gallery => gallery.LastOrDefault() ?? "")
                .Token(token => token.StartsWith("Concat:") ? token.Substring("Concat:".Length) : null, Concatenate);
        }

        private HtmlString Concatenate(string formatAndSeparator, IEnumerable<string> values) {
            var lastComma = formatAndSeparator.LastIndexOf(',');
            string format, separator;
            if (lastComma == -1) {
                format = formatAndSeparator;
                separator = "";
            }
            else {
                format = formatAndSeparator.Substring(0, lastComma);
                separator = lastComma + 1 < formatAndSeparator.Length ? formatAndSeparator.Substring(lastComma + 1) : "";
            }
            return new HtmlString(String.Join(separator, values.Select(s => String.Format(format, s))));
        }
    }
}