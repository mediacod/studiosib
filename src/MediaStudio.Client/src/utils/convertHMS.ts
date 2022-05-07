export function convertHMS(value: any) {
    const sec = parseInt(value, 10); // convert value to number if it's string
    let hours: any = Math.floor(sec / 3600); // get hours
    let minutes: any = Math.floor((sec - (hours * 3600)) / 60); // get minutes
    let seconds: any = sec - (hours * 3600) - (minutes * 60); //  get seconds
    if (hours > 0) {
        if (hours < 10) {
            hours = "0" + hours;
        }
    }
    if (minutes < 10) {
        minutes = "0" + minutes;
    }
    if (seconds < 10) {
        seconds = "0" + seconds;
    }

    return hours <= 0
        ? `${minutes}:${seconds}`
        : `${hours}:${minutes}:${seconds}`
}
