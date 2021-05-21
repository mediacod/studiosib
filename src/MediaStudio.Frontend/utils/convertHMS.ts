export function convertHMS(value): string {
    const sec: number = parseInt(value, 10); // convert value to number if it's string
    let hours = Math.floor(sec / 3600); // get hours
    let minutes = Math.floor((sec - (hours * 3600)) / 60); // get minutes
    let seconds = sec - (hours * 3600) - (minutes * 60); //  get seconds

    let hoursString = `${hours}`
    let minutesString = `${minutes}`
    let secondsString = `${seconds}`

    if (hours > 0) {
        if (hours < 10) {
            hoursString = "0" + hoursString;
        }
    }
    if (minutes < 10) {
        minutesString = "0" + minutesString;
    }
    if (seconds < 10) {
        secondsString = "0" + secondsString;
    }

    return hours <= 0
        ? `${minutesString}:${secondsString}`
        : `${hoursString}:${minutesString}:${secondsString}`
}
