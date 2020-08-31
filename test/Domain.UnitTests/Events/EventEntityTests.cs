using System;
using System.Collections.Generic;
using System.Linq;
using Eventshuffle.Domain.Events;
using NodaTime;
using Shouldly;
using Xunit;

namespace EVentshuffle.Domain.UnitTests
{
    public class EventEntityTests
    {
        [Fact]
        public void Vote_ShouldThrowArgumentException_WhenNameIsNull()
        {
            var eventEntity = new EventEntity();
            string name = null;
            IEnumerable<LocalDate> dates = Enumerable.Empty<LocalDate>();

            Should.Throw<ArgumentException>(() => eventEntity.Vote(name, dates));
        }
        
        [Fact]
        public void Vote_ShouldThrowArgumentException_WhenNameIsEmpty()
        {
            var eventEntity = new EventEntity();
            string name = string.Empty;
            IEnumerable<LocalDate> dates = Enumerable.Empty<LocalDate>();

            Should.Throw<ArgumentException>(() => eventEntity.Vote(name, dates));
        }
        
        [Fact]
        public void Vote_ShouldThrowArgumentException_WhenNameIsWhitespace()
        {
            var eventEntity = new EventEntity();
            string name = " ";
            IEnumerable<LocalDate> dates = Enumerable.Empty<LocalDate>();

            Should.Throw<ArgumentException>(() => eventEntity.Vote(name, dates));
        }

        [Fact]
        public void Vote_ShouldThrowArgumentNullException_WhenDatesIsNull()
        {
            var eventEntity = new EventEntity();
            string name = "Name";
            IEnumerable<LocalDate> dates = null;

            Should.Throw<ArgumentException>(() => eventEntity.Vote(name, dates));
        }

        [Fact]
        public void Vote_ShouldAddVote_WhenNoVoteForNameAndDateExists()
        {
            var date = new LocalDate(2000, 01, 01);
            var dates = new List<LocalDate> { date };
            var eventEntity = new EventEntity("Event", dates);
            var name = "Voter";

            eventEntity.Vote(name, dates);

            eventEntity.DateOptions.Single().Date.ShouldBe(date);
            eventEntity.DateOptions.Single().Votes.ShouldHaveSingleItem();
            eventEntity.DateOptions.Single().Votes.Single().Name.ShouldBe(name);
        }
        
        [Fact]
        public void Vote_ShouldNotAddAnotherVote_WhenVoteForNameAndDateExists()
        {
            var date = new LocalDate(2000, 01, 01);
            var dates = new List<LocalDate> { date };
            var eventEntity = new EventEntity("Event", dates);
            var name = "Voter";

            eventEntity.Vote(name, dates);
            eventEntity.Vote(name, dates);

            eventEntity.DateOptions.ShouldHaveSingleItem();
            eventEntity.DateOptions.Single().Date.ShouldBe(date);
            eventEntity.DateOptions.Single().Votes.ShouldHaveSingleItem();
            eventEntity.DateOptions.Single().Votes.Single().Name.ShouldBe(name);
        }

        [Fact]
        public void Vote_ShouldNotAddVote_WhenDateOptionDoesNotExist()
        {
            var date = new LocalDate(2000, 01, 01);
            var dates = new List<LocalDate> { date };
            var votingDate = new LocalDate(2002, 02, 02);
            var votingDates = new List<LocalDate> { votingDate };
            var eventEntity = new EventEntity("Event", dates);
            var name = "Voter";

            eventEntity.Vote(name, votingDates);

            eventEntity.DateOptions.ShouldHaveSingleItem();
            eventEntity.DateOptions.Single().Date.ShouldBe(date);
            eventEntity.DateOptions.Single().Votes.ShouldBeEmpty();
        }

        [Fact]
        public void GetSuitableDateOptionsForAllVoters_ShouldFindDate_WhenAllVotedThatDate()
        {
            var date1 = new LocalDate(2000, 01, 01);
            var date2 = new LocalDate(2002, 02, 02);
            var dates = new List<LocalDate> { date1, date2 };
            var votingDates = new List<LocalDate> { date1 };
            var name1 = "Name1";
            var name2 = "Name2";
            var eventEntity = new EventEntity("Event", dates);
            eventEntity.Vote(name1, votingDates);
            eventEntity.Vote(name2, votingDates);

            var result = eventEntity.GetSuitableDateOptionsForAllVoters();

            result.ShouldHaveSingleItem();
            result.Single().Date.ShouldBe(date1);
            result.Single().Votes.Count.ShouldBe(2);
        }

        [Fact]
        public void GetSuitableDateOptionsForAllVoters_ShouldNotFindDates_WhenNobodyVoted()
        {
            var date1 = new LocalDate(2000, 01, 01);
            var date2 = new LocalDate(2002, 02, 02);
            var dates = new List<LocalDate> { date1, date2 };
            var eventEntity = new EventEntity("Event", dates);

            var result = eventEntity.GetSuitableDateOptionsForAllVoters();

            result.ShouldBeEmpty();
        }
    }
}
