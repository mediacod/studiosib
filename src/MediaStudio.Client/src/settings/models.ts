export interface ICells {
    colorCode?: string
    countTrack?: number
    idObject: number
    idTypeCell: 2
    linkCover?: string
    name: string
    orderSection?: number
}

export interface  ISectionData {
    cells: ICells[]
    nameSection: string
}

export interface ISection {
    sectionData: ISectionData
    openSection?: (index: number)=>void
    index?: number
}