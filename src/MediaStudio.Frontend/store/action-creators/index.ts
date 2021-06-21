import * as PlayerActionCreators from '../action-creators/player'
import * as PageActionsCreator from '../action-creators/page'
import * as AlbumPageActionsCreator from '../action-creators/albumPage'

export default {
    ...PlayerActionCreators,
    ...PageActionsCreator,
    ...AlbumPageActionsCreator
}
