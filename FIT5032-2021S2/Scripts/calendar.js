function generateCalendar(events) {
    $('#calendar').fullCalendar({
        defaultView: 'month',
        contentHeight: 600,
        businessHours: true,
        timeFormat:"h(:mm)a",
        header: {
            left: "month,agendaWeek, today",
            center: "title",
            right:"prevYear, prev,next, nextYear"
        },
        events: events,
        eventClick: function (event) {
            console.log(event);
            $("#details").css("display", "block");
            $("#eventName").text(event.title);
            $("#storeName").text(event.store);
            $("#startTime").text(moment(event.start).format("DD MMM YYYY hh:mm a"));
            $("#endTime").text(moment(event.end).format("DD MMM YYYY hh:mm a"));
            if (moment(event.start) > $.now()) {
                $("#bookLink").text("Book now");
                var url = "/BookEvents/BookEvent/" + event.eventId;
                $("#bookLink").attr("href", url);
            } else {
                $("#bookLink").text("Event finished");
                $("#bookLink").attr("onclick", false);
                var url = "#";
                $("#bookLink").attr("href", url);
            }
        }
    });
}