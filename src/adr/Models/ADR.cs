using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace adr.Models
{
    public class ADR
    {
        public int? Number { get; set; }
        public string Title { get; set; }
        public DateTime CreatedUTC { get; set; }
        public string Status { get; set; }
        public Dictionary<string, string> References { get; set; }
        public string Context { get; set; }
        public string Decision { get; set; }
        public string Consequences { get; set; }

        public ADR()
        {
            CreatedUTC = DateTime.UtcNow;
            References = new Dictionary<string, string>();
            Status = "Pending";
        }

        public string GetContent()
        {
            var content = GetContentTemplate();
            content = content.Replace("{{NUMBER}}", Number.Value.ToString("XXXX"));
            content = content.Replace("{{TITLE}}", Title);
            content = content.Replace("{{DATE}}", CreatedUTC.ToString("yyyy-MM-dd HH:mm:ss"));
            content = content.Replace("{{STATUS}}", Status);
            content = content.Replace("{{REFERENCES}}", string.Join(" ",
                                    References.Select(
                                        x => "- [" + x.Key + "](" + x.Value + ")" + Environment.NewLine
                                        ).ToArray()));
            content = content.Replace("{{CONTEXT}}", Context);
            content = content.Replace("{{DECISION}}", Decision);
            content = content.Replace("{{CONSEQUENCES}}", Consequences);

            return content;
        }

        public string GetContentTemplate()
        {
            return @"# {{NUMBER}}. {{TITLE}}

Date: {{DATE}}

## Status

{{STATUS}}

{{REFERENCES}}

## Context

The issue motivating this decision, and any context that influences or constrains the decision.
{{CONTEXT}}

## Decision

The change that we're proposing or have agreed to implement.
{{DECISION}}

## Consequences

What becomes easier or more difficult to do and any risks introduced by the change that will need to be mitigated.
{{CONSEQUENCES}}
";
        }
    }
}