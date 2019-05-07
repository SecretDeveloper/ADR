# ADR
ADR - Architecture Decision Records.  A command line tool for creating ADRs.

## Spec
 - Records will be stored in an "ADR" directory
 - ADR directory will be created when the 'init' command is executed
   - The current directory and parent directories will be scanned to determine if they contain an "ADR" directory.  If so then the init will abend with error.
   - The init command will create the ADR directory and any initial files including templates


## Commands
 - 'adr init' -- Initialize new ADR directory
 - 'adr new [title]' -- Create new entry.
 - 'adr new -s 5 [title]' -- Indicates that ADR 5 is superceded by this new entry, each entry will link reference the other.
 - 'adr new -a 5 [title]' -- Indicates that ADR 5 is amended by this new entry, each entry will link reference the other.
 - 'adr status' -- Lists the ADRs and their status.
 - 'adr generate' -- Generates a single document from the collection of ADRs.
 - 'adr link [SOURCE] [LINK_TYPE] [TARGET] [TEXT]' -- Adds references between 2 ADR records.
 - 'adr list' -- List all ADR records.
 - 'adr help' -- Show Help screen.
