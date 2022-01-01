﻿using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.ReportGenerator.MultiGenerator
{
    public class MultiGenerator
    {
        private readonly MultiReportItemFactory _itemFactory;
        private readonly IReportGenerator _generator;

        public MultiGenerator(MultiReportItemFactory itemFactory, IReportGenerator generator)
        {
            _itemFactory = itemFactory;
            _generator = generator;
        }

        public IReadOnlyCollection<FileDescriptor> Generate(ISourceCodeProvider sourceCodeProvider)
        {
            var result = new List<FileDescriptor>();

            foreach (MultiReportItem multiReportItem in _itemFactory.Split(sourceCodeProvider))
            {
                FileDescriptor report = _generator.Generate(sourceCodeProvider.GetFiles(multiReportItem.Filter), multiReportItem.ExtendedInfo);
                result.Add(report);
            }

            return result;
        }
    }
}