import {useMutation, useQueryClient} from "react-query";
import {USER_FAVOURITES_TRACK} from "../api/api";


export const useUpdateFavoriteTrackQuery = () => {

    const queryClient = useQueryClient()

    const addFavoriteTrackMutation = useMutation(
        (id) => USER_FAVOURITES_TRACK.ADD(id),
        {
            // Когда вызывается mutate:
            onMutate: async (id: any) => {
                // Отмените все исходящие повторные выборки
                await queryClient.cancelQueries(['favoritesTrack'])

                // Сохраняем предыдущие значения
                const previous: any = queryClient.getQueryData(['favoritesTrack'])

                // Оптимистично обновите до нового значения
                if (previous) {
                    queryClient.setQueryData(['favoritesTrack'], {
                        ...previous,
                        data: [...previous.data, {idTrack: id}],
                    })
                }

                return { previous }
            },
            // If the mutation fails, use the context returned from onMutate to roll back
            onError: (err, variables, context) => {
                if (context?.previous) {
                    queryClient.setQueryData(['favoritesTrack'], context.previous)
                }
            },
            // Always refetch after error or success:
            onSettled: () => {
                queryClient.invalidateQueries(['favoritesTrack'])
            },
        }
    )

    const removeFavoriteTrackMutation = useMutation(
        (id) => USER_FAVOURITES_TRACK.REMOVE(id),
        {
            // Когда вызывается mutate:
            onMutate: async (id: any) => {
                // Отмените все исходящие повторные выборки
                await queryClient.cancelQueries(['favoritesTrack'])

                // Сохраняем предыдущие значения
                const previous: any = queryClient.getQueryData(['favoritesTrack'])

                // Оптимистично обновите до нового значения
                if (previous) {
                    queryClient.setQueryData(['favoritesTrack'], {
                        ...previous,
                        data: [...previous?.data?.filter((p: any) => p.idTrack !== id)]
                    })
                }

                return { previous }
            },
            // If the mutation fails, use the context returned from onMutate to roll back
            onError: (err, variables, context) => {
                if (context?.previous) {
                    queryClient.setQueryData(['favoritesTrack'], context.previous)
                }
            },
            // Always refetch after error or success:
            onSettled: () => {
                queryClient.invalidateQueries(['favoritesTrack'])
            },
        }
    )



    return {addTodoMutation: addFavoriteTrackMutation, removeTodoMutation: removeFavoriteTrackMutation}
};