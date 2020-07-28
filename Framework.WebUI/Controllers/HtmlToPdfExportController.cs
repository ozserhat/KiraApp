using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using Image = iTextSharp.text.Image;
using HtmlAgilityPack;
using System.Web.Mvc;
using System;
using System.IO;
using System.Collections.Generic;

namespace Framework.WebUI.Controllers
{
    public class HtmlToPdfExportController : Controller
    {
        // GET: HtmlToPdfExport
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string ExportAreaHtml, bool Indir = false, Guid? Guid = null)
        {
            if (string.IsNullOrEmpty(ExportAreaHtml))
                return null;
            Rectangle pazeSize = null;
            float marginLeft = 0;
            float marginRight = 0;
            float marginTop = 0;
            float marginBottom = 0;
            bool logoGoster = false;
            if (Indir)
            {
                pazeSize = PageSize.A4.Rotate();
                marginLeft = 10;
                marginRight = 10;
                marginTop = 15;
                marginBottom = 15;
                logoGoster = true;
            }
            else
            {
                pazeSize = PageSize.A4.Rotate();
                marginLeft = 10;
                marginRight = 10;
                marginTop = 70;
                marginBottom = 25;
            }

            //Generate PDF
            using (var document = new Document(pazeSize, marginLeft, marginRight, marginTop, marginBottom))
            {
                //define output control HTML
                var memStream = new MemoryStream();
                var hDocument = new HtmlDocument()
                {
                    OptionWriteEmptyNodes = true,
                    OptionAutoCloseOnEnd = true
                };
                hDocument.LoadHtml(ExportAreaHtml);
                var closedTags = hDocument.DocumentNode.WriteTo();
                TextReader xmlString = new StringReader(closedTags);
                var writer = PdfWriter.GetInstance(document, memStream);
                if (logoGoster)
                {
                    var image = iTextSharp.text.Image.GetInstance(Server.MapPath("~/content/img/csb-logo.png"));
                    image.ScaleAbsolute(75f, 75f);
                    writer.PageEvent = new ITextEvents(image);
                }

                //open doc
                document.Open();

                BaseFont STF_Helvetica_Turkish = BaseFont.CreateFont("Helvetica", "Cp1254", BaseFont.NOT_EMBEDDED);
                Font yenifont = new Font(STF_Helvetica_Turkish, 6, Font.NORMAL, BaseColor.BLACK);


                // Set factories
                var htmlContext = new HtmlPipelineContext(null);


                var tagProcessors = (DefaultTagProcessorFactory)Tags.GetHtmlTagProcessorFactory();
                tagProcessors.RemoveProcessor(HTML.Tag.IMG); // remove the default processor
                tagProcessors.AddProcessor(HTML.Tag.IMG, new CustomImageTagProcessor());
                htmlContext.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(tagProcessors);

                // Set css
                ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                cssResolver.AddCssFile(HttpContext.Server.MapPath("~/Content/bootstrap.min.css"), true);
                cssResolver.AddCssFile(HttpContext.Server.MapPath("~/Content/bootstrap-theme.min.css"), true);
                cssResolver.AddCssFile(HttpContext.Server.MapPath("~/Content/font-awesome.min.css"), true);
                cssResolver.AddCssFile(HttpContext.Server.MapPath("~/Content/smartadmin-production-plugins.min.css"), true);
                cssResolver.AddCssFile(HttpContext.Server.MapPath("~/Content/smartadmin-production.min.css"), true);
                cssResolver.AddCssFile(HttpContext.Server.MapPath("~/Content/smartadmin-skins.min.css"), true);
                cssResolver.AddCssFile(HttpContext.Server.MapPath("~/Content/Site.css"), true);

                IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));
                var worker = new XMLWorker(pipeline, true);

                var xmlParse = new XMLParser(true, worker);
                xmlParse.Parse(xmlString);
                xmlParse.Flush();

                document.Close();
                document.Dispose();
                FileContentResult result = null;

                if (Indir)
                {
                    result = File(memStream.ToArray(), "application/pdf", Guid + ".pdf");
                }
                else
                {
                    result = File(memStream.ToArray(), "application/pdf");
                }

                memStream.Dispose();
                return result;
            }
        }

        public class CustomImageTagProcessor : iTextSharp.tool.xml.html.Image
        {
            public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent)
            {
                IDictionary<string, string> attributes = tag.Attributes;
                string src;
                if (!attributes.TryGetValue(HTML.Attribute.SRC, out src))
                    return new List<IElement>(1);

                if (string.IsNullOrEmpty(src))
                    return new List<IElement>(1);

                if (src.StartsWith("data:image/", StringComparison.InvariantCultureIgnoreCase))
                {
                    // data:[<MIME-type>][;charset=<encoding>][;base64],<data>
                    var base64Data = src.Substring(src.IndexOf(",") + 1);
                    var imagedata = Convert.FromBase64String(base64Data);
                    var image = iTextSharp.text.Image.GetInstance(imagedata);

                    var list = new List<IElement>();
                    var htmlPipelineContext = GetHtmlPipelineContext(ctx);
                    list.Add(GetCssAppliers().Apply(new Chunk((iTextSharp.text.Image)GetCssAppliers().Apply(image, tag, htmlPipelineContext), 0, 0, true), tag, htmlPipelineContext));
                    return list;
                }
                else
                {
                    return base.End(ctx, tag, currentContent);
                }
            }
        }

        public class ITextEvents : PdfPageEventHelper
        {
            // This is the contentbyte object of the writer
            PdfContentByte cb;

            // we will put the final number of pages in a template
            PdfTemplate headerTemplate, footerTemplate;

            // this is the BaseFont we are going to use for the header / footer
            BaseFont bf = null;

            // This keeps track of the creation time
            DateTime PrintTime = DateTime.Now;

            #region Fields
            private string _header;
            #endregion

            private readonly iTextSharp.text.Image _image;

            public ITextEvents(Image image1)
            {
                _image = image1;
            }

            #region Properties
            public string Header
            {
                get { return _header; }
                set { _header = value; }
            }
            #endregion

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    PrintTime = DateTime.Now;
                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb = writer.DirectContent;
                    headerTemplate = cb.CreateTemplate(100, 100);
                    footerTemplate = cb.CreateTemplate(50, 50);

                }
                catch (DocumentException de)
                {
                }
                catch (System.IO.IOException ioe)
                {
                }
            }

            public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
            {
                base.OnEndPage(writer, document);
                iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9f, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(16, 78, 139));
                iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(16, 78, 139));
                iTextSharp.text.Font baseFontSmall = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0));
                //Phrase p1Header = new Phrase("T.C. ÇEVRE VE ŞEHİRCİLİK BAKALNIĞI", baseFontBig);

                PdfPTable pdfTab = new PdfPTable(1);

                //Row 1
                PdfPCell pdfCell1 = new PdfPCell(_image);
                //PdfPCell pdfCell2 = new PdfPCell(p1Header);
                //PdfPCell pdfCell3 = new PdfPCell(new Phrase("Tarih:" + PrintTime.ToShortDateString(), baseFontSmall));

                //Row 2
                //PdfPCell pdfCell4 = new PdfPCell(new Phrase("Müteahhit Yeterlik Sistemi", baseFontNormal));
                //PdfPCell pdfCell5 = new PdfPCell(new Phrase("Saat:" + string.Format("{0:t}", DateTime.Now), baseFontSmall));

                pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
                //pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                //pdfCell3.HorizontalAlignment = Element.ALIGN_RIGHT;
                //pdfCell4.HorizontalAlignment = Element.ALIGN_CENTER;
                //pdfCell5.HorizontalAlignment = Element.ALIGN_RIGHT;

                pdfCell1.PaddingLeft = 15;
                pdfCell1.PaddingTop = 10;
                //pdfCell3.PaddingRight = 10;
                //pdfCell3.PaddingTop = 20;
                //pdfCell5.PaddingRight = 20;
                //pdfCell2.PaddingTop = 20;


                pdfCell1.VerticalAlignment = Element.ALIGN_LEFT;
                //pdfCell2.VerticalAlignment = Element.ALIGN_TOP;
                //pdfCell3.VerticalAlignment = Element.ALIGN_RIGHT;
                //pdfCell4.VerticalAlignment = Element.ALIGN_TOP;
                //pdfCell5.VerticalAlignment = Element.ALIGN_RIGHT;

                pdfCell1.Rowspan = 2;

                pdfCell1.Border = 0;
                //pdfCell2.Border = 0;
                //pdfCell3.Border = 0;
                //pdfCell4.Border = 0;
                //pdfCell5.Border = 0;

                pdfTab.AddCell(pdfCell1);
                //pdfTab.AddCell(pdfCell2);
                //pdfTab.AddCell(pdfCell3);
                //pdfTab.AddCell(pdfCell4);
                //pdfTab.AddCell(pdfCell5);

                pdfTab.TotalWidth = document.PageSize.Width;
                pdfTab.WidthPercentage = 100;
                pdfTab.WriteSelectedRows(0, -1, 0, document.PageSize.Height - 9, writer.DirectContent);
            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                headerTemplate.BeginText();
                headerTemplate.SetFontAndSize(bf, 12);
                headerTemplate.SetTextMatrix(0, 0);
                headerTemplate.ShowText((writer.PageNumber - 1).ToString());
                headerTemplate.EndText();

                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 12);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber - 1).ToString());
                footerTemplate.EndText();
            }
        }
    }
}