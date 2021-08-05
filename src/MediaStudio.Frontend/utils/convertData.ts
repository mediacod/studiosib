export const convertDataSearch = (data) => {

    const cycle = (alb, idTypeCell) => {
        return alb.map(a => ({...a, idObject: a.idAlbum || a.idPlaylist, idTypeCell}))
    }

    return {
        albums: data.albums ? cycle(data.albums, 1) : [],
        perfomers: data.perfomers ? cycle(data.perfomers, 3) : [],
        playlists: data.playlists ? cycle(data.playlists, 2) : [],
        tracks: data.tracks ? [...data.tracks] : []
    }
}