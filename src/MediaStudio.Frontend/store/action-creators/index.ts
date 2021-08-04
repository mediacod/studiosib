import * as PlayerActionCreators from '../action-creators/player'
import * as PageActionsCreator from '../action-creators/page'
import * as AlbumPageActionsCreator from '../action-creators/albumPage'
import * as SearchActionsCreator from '../action-creators/search'

export default {
    ...PlayerActionCreators,
    ...PageActionsCreator,
    ...AlbumPageActionsCreator,
    ...SearchActionsCreator,
}
