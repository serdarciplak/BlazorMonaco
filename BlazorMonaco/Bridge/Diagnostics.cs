using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMonaco.Bridge
{
    /// <summary>
    /// Immutable span represented by a pair of line number and index within the line.
    /// </summary>
    [DataContract]
    public class LinePositionSpan
    {

        /// <summary>
        /// Creates <see cref="LinePositionSpan"/>.
        /// </summary>
        /// <param name="start">Start position.</param>
        /// <param name="end">End position.</param>
        /// <exception cref="ArgumentException"><paramref name="end"/> precedes <paramref name="start"/>.</exception>
        public LinePositionSpan(LinePosition start, LinePosition end)
        {

            Start = start;
            End = end;
        }

        /// <summary>
        /// Gets the start position of the span.
        /// </summary>
        [DataMember(Order = 0)]
        public LinePosition Start { get; set; }

        /// <summary>
        /// Gets the end position of the span.
        /// </summary>
        [DataMember(Order = 1)]
        public LinePosition End { get; set; }

    }
    [DataContract]
    public class DiagnosticModel
    {
        [DataMember(Order = 0)]
        public int Severity { get; set; }

        [DataMember(Order = 1)]
        public bool IsSuppressed { get; set; }

        [DataMember(Order = 2)]
        public int WarningLevel { get; set; }

        [DataMember(Order = 3)]
        public string Message { get; set; }

        [DataMember(Order = 4)]
        public LinePositionSpan LinePositionSpan { get; set; }

        [DataMember(Order = 5)]
        public bool IsWarningAsError { get; set; }

        [DataMember(Order = 6)]
        public bool IsUnsuppressedError { get; set; }
    }
    public static class DiagnosticModelExtensions
    {
        public static DiagnosticModel ToDiagnosticModel(this Diagnostic diagnostic)
        {
            return new DiagnosticModel
            {
                Severity = diagnostic.Severity.GetSeverity(),
                IsSuppressed = diagnostic.IsSuppressed,
                WarningLevel = diagnostic.WarningLevel,
                LinePositionSpan = diagnostic.Location.GetLineSpan().Span.ToLinePositionSpan(),
                Message = diagnostic.Descriptor.Description.ToString(),
                //Properties = diagnostic.Properties.ToDictionary(x => x.Key, x => x.Value),
                IsWarningAsError = diagnostic.IsWarningAsError,
                IsUnsuppressedError = !diagnostic.IsSuppressed,
            };
        }
        public static int GetSeverity(this DiagnosticSeverity severity)
        {

            if (severity == DiagnosticSeverity.Hidden) return 1;
            if (severity == DiagnosticSeverity.Info) return 2;
            if (severity == DiagnosticSeverity.Warning) return 4;
            if (severity == DiagnosticSeverity.Error) return 8;
            throw new Exception("Unknown diagnostic severity.");
        }
        public static IEnumerable<DiagnosticModel> ToDiagnosticModels(this ImmutableArray<Diagnostic> diagnostics)
        {
            return diagnostics.Select(diagnostic =>
               diagnostic.ToDiagnosticModel());
        }

        public static DiagnosticResponse ToDiagnosticResponse(this ImmutableArray<Diagnostic> diagnostics)
        {
            return new DiagnosticResponse
            {
                Diagnostics = diagnostics.ToDiagnosticModels().ToList()
            };
        }

        public static LinePosition ToLinePosition(this Microsoft.CodeAnalysis.Text.LinePosition value)
            => new LinePosition(value.Line, value.Character);

        public static LinePositionSpan ToLinePositionSpan(this Microsoft.CodeAnalysis.Text.LinePositionSpan value)
            => new LinePositionSpan(value.Start.ToLinePosition(), value.End.ToLinePosition());
    }
    [DataContract]
    public class DiagnosticResponse
    {
        [DataMember(Order = 0)]
        public List<DiagnosticModel> Diagnostics { get; set; }
    }
}
