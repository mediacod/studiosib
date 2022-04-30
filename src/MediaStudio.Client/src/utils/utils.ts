export const getTypeCell = (cell: any) => {
    if(cell?.idPlaylist && cell?.isPublic === false) {
        return 2
    }
    if(cell?.idTypeCell) {
        return cell.idTypeCell
    }
    if(cell?.idAlbum) {
        return 1
    }
    if(cell?.idPlaylist) {
        return 2
    }
}