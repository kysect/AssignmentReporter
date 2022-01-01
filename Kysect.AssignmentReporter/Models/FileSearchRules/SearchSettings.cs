using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Kysect.AssignmentReporter.Common;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
    public class SearchSettings
    {
        public List<string> WhiteFileNames { get; set; } = new List<string>();
        public List<string> WhiteFileFormats { get; set; } = new List<string>();
        public List<Regex> WhiteDirectories { get; set; } = new List<Regex>();
        public List<string> BlackFileNames { get; set; } = new List<string>();
        public List<string> BlackFileFormats { get; set; } = new List<string>();
        public List<Regex> BlackDirectories { get; set; } = new List<Regex>();

        public Reasonable<bool> FileIsAcceptable(string fileName)
        {
            if (WhiteFileNames.Count != 0 && !WhiteFileNames.Contains(fileName))
            {
                return Reasonable.Create(false, $"File {fileName} do not registered in white list");
            }

            if (BlackFileNames.Contains(fileName))
            {
                return Reasonable.Create(false, $"File {fileName} registered in black list");
            }

            return Reasonable.Create(true);
        }

        public Reasonable<bool> FormatIsAcceptable(string fileFormat)
        {
            if (WhiteFileFormats.Count != 0 && !WhiteFileFormats.Contains(fileFormat))
            {
                return Reasonable.Create(false, $"File format '{fileFormat}' do not registered in white list");
            }

            if (BlackFileFormats.Contains(fileFormat))
            {
                return Reasonable.Create(false, $"File format '{fileFormat}' registered in black list");
            }

            return Reasonable.Create(true);
        }

        public Reasonable<bool> DirectoryIsAcceptable(PartialPath directoryPath)
        {
            Regex matchedBlackMask = BlackDirectories.FirstOrDefault(mask => mask.IsMatch(directoryPath.Path));
            if (matchedBlackMask != null)
            {
                return Reasonable.Create(false, $"Directory {directoryPath.Path} matched with black list mask: {matchedBlackMask}");
            }

            Regex matchedWhiteMask = WhiteDirectories.FirstOrDefault(mask => mask.IsMatch(directoryPath.Path));
            if (WhiteDirectories.Any() && matchedWhiteMask is null)
            {
                return Reasonable.Create(false, $"Directory {directoryPath.Path} does not matched any white mask");
            }

            return Reasonable.Create(true);
        }
    }
}