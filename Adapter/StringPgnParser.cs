using System;
using System.Collections.Generic;
using System.Diagnostics;
using chess_validator.Core;
using chess_validator.Core.Exceptions;

namespace chess_validator.Adapter
{
    public class StringPgnParser : IPgnParser
    {
        private class TagNotFoundException : Exception
        {
            private readonly string _tagName;

            public TagNotFoundException(string tagName)
            {
                _tagName = tagName;
            }

            public string TagName => _tagName;
        }

        private class MissingEndOfTagException : Exception
        {
            private readonly string _tagName;

            public MissingEndOfTagException(string tagName)
            {
                _tagName = tagName;
            }

            public string TagName => _tagName;
        }

        private readonly string _content;

        public StringPgnParser(string content)
        {
            _content = content;
        }

        public Match Parse()
        {
            try
            {
                var @event = GetTagValue("Event");
                var site = GetTagValue("Site");
                var date = ParseDate(GetTagValue("Date"));
                var round = int.Parse(GetTagValue("Round"));
                var white = ParsePlayer(GetTagValue("White"));
                var black = ParsePlayer(GetTagValue("Black"));
                var result = ParseResult(GetTagValue("Result"), white, black);
                var moves = new LinkedList<PairMovement>();

                return new Match(@event, site, date, round, white, black, result, moves);
            }
            catch (TagNotFoundException e)
            {
                throw new MissingTagException($"Tag {e.TagName} is missing!");
            }
            catch (MissingEndOfTagException e)
            {
                throw new CannotParsePgnContentException($"Missing end of a {e.TagName} tag!");
            }
        }

        private string GetTagValue(string tagName)
        {
            var startIndex = _content.IndexOf($"[{tagName} \"", StringComparison.OrdinalIgnoreCase);
            if (startIndex == -1) throw new TagNotFoundException(tagName);

            var endIndex = _content.IndexOf("\"]", startIndex, StringComparison.Ordinal);
            if (endIndex == -1) throw new MissingEndOfTagException(tagName);

            var tag = _content.Substring(startIndex, endIndex - startIndex + 2);

            return tag.Replace($"[{tagName} \"", "").Replace("\"]", "");
        }

        private DateTime ParseDate(string source)
        {
            Console.WriteLine(source);
            return DateTime.Parse(source.Replace(".", "-").Replace("??", "01"));
        }

        private Player ParsePlayer(string source)
        {
            var names = source.Split(',');

            var firstName = names[1].Trim(' ');
            var lastName = names[0].Trim(' ');

            return new Player(firstName, lastName);
        }

        private Result ParseResult(string source, Player white, Player black)
        {
            switch (source)
            {
                case "1-0":
                    return new Result(white);
                case "0-1":
                    return new Result(black);
                case "1-2/1-2":
                    return new Result(GameState.DRAW);
                default:
                    return new Result(GameState.STILL_PLAYING);
            }
        }
    }
}