using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Kysect.CommonLib.Paths;
using Kysect.CommonLib.Reasons;

namespace Kysect.AssignmentReporter.ReportGenerator;

public class SearchSettings
{
    public List<string> WhiteFileNames { get; set; } = new List<string>();
    public List<string> WhiteFileFormats { get; set; } = new List<string>();
    public List<Regex> WhiteDirectories { get; set; } = new List<Regex>();
    public List<string> BlackFileNames { get; set; } = new List<string>();
    public List<string> BlackFileFormats { get; set; } = new List<string>();
    public List<Regex> BlackDirectories { get; set; } = new List<Regex>();

    public Reason<bool> FileIsAcceptable(string fileName)
    {
        if (WhiteFileNames.Count != 0 && !WhiteFileNames.Contains(fileName))
        {
            return Reason.Create(false, $"File {fileName} do not registered in white list");
        }

        if (BlackFileNames.Contains(fileName))
        {
            return Reason.Create(false, $"File {fileName} registered in black list");
        }

        return Reason.Create(true);
    }

    public Reason<bool> FormatIsAcceptable(string fileFormat)
    {
        if (WhiteFileFormats.Count != 0 && !WhiteFileFormats.Contains(fileFormat))
        {
            return Reason.Create(false, $"File format '{fileFormat}' do not registered in white list");
        }

        if (BlackFileFormats.Contains(fileFormat))
        {
            return Reason.Create(false, $"File format '{fileFormat}' registered in black list");
        }

        return Reason.Create(true);
    }

    public Reason<bool> DirectoryIsAcceptable(PartialPath directoryPath)
    {
        Regex matchedBlackMask = BlackDirectories.FirstOrDefault(mask => mask.IsMatch(directoryPath.Value));
        if (matchedBlackMask != null)
        {
            return Reason.Create(false, $"Directory {directoryPath.Value} matched with black list mask: {matchedBlackMask}");
        }

        Regex matchedWhiteMask = WhiteDirectories.FirstOrDefault(mask => mask.IsMatch(directoryPath.Value));
        if (WhiteDirectories.Any() && matchedWhiteMask is null)
        {
            return Reason.Create(false, $"Directory {directoryPath.Value} does not matched any white mask");
        }

        return Reason.Create(true);
    }
}