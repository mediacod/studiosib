import {useMutation, useQuery, useQueryClient} from "react-query";
import {USER_PLAYLIST_API} from "../api/api";
import {Toast} from "antd-mobile";

export const useUserPlaylistOneQuery = (id?: String) => {

    const queryClient = useQueryClient()
    const getQueryKey = (playlistId = id) => ['userPlaylist', id]
    const queryKey = getQueryKey()

    const {data} = useQuery(queryKey, ()=>USER_PLAYLIST_API.GET_ONE(id), {enabled: !!id})

    /**
     * Добавляет idPlaylist в каждый трек
     */
    const addIdPlaylistToTrack = {...data?.data, tracks: data?.data?.tracks?.map((item: any) => ({...item, attributes: {idPlaylist: id, userPlaylist: true}}))}

    /**
     * Удаляет треки из плейлиста
     */
    const {mutate: addTrack, isError: isErrorAdd, isSuccess: isSuccessAdd, isLoading: isLoadingAdd} = useMutation(
        (data) => USER_PLAYLIST_API.ADD_TRACK(data),
        {
            // Когда вызывается mutate:
            onMutate: async (newData: any) => {
                const queryKey = getQueryKey(newData?.idPlaylist)
                // Отмените все исходящие повторные выборки
                await queryClient.cancelQueries(queryKey)

                // Сохраняем предыдущие значения
                const previous: any = queryClient.getQueryData(queryKey)

                // Оптимистично обновите до нового значения
                if (previous) {
                    queryClient.setQueryData(queryKey, {
                        ...previous, data: [newData, ...previous.data],
                    })
                }

                return { previous }
            },
            // If the mutation fails, use the context returned from onMutate to roll back
            onError: (err, variables, context) => {
                if (context?.previous) {
                    queryClient.setQueryData(queryKey, context.previous)
                }
                Toast.show({
                    icon: 'fail',
                    content: 'Ошибка добавления',
                })
            },
            // Always refetch after error or success:
            onSettled: () => {
                queryClient.invalidateQueries(queryKey)
            },
            onSuccess: () => {
                Toast.show({
                    icon: 'success',
                    content: 'Добален в плейлист',
                })
            }
        }
    )

    const {mutate: removeTrack, isError: isErrorRemove, isSuccess: isSuccessRemove, isLoading: isLoadingRemove} = useMutation(
        (data) => USER_PLAYLIST_API.REMOVE_TRACK(data),
        {
            // Когда вызывается mutate:
            onMutate: async (newData: any) => {

                const queryKey = getQueryKey(newData?.idPlaylist);

                // Отмените все исходящие повторные выборки
                await queryClient.cancelQueries(queryKey)

                // Сохраняем предыдущие значения
                const previous: any = queryClient.getQueryData(queryKey)
                console.log(previous)
                //Оптимистично обновите до нового значения
                if (previous) {
                    queryClient.setQueryData(queryKey, {
                        ...previous,
                        tracks: [...previous?.tracks?.filter((p: any) => p.idTrackToPlaylist !== id)]
                    })
                }

                return { previous }
            },
            // If the mutation fails, use the context returned from onMutate to roll back
            onError: (err, variables, context) => {
                if (context?.previous) {
                    queryClient.setQueryData(queryKey, context.previous)
                }
                Toast.show({
                    icon: 'fail',
                    content: 'Ошибка удаления',
                })
            },
            // Always refetch after error or success:
            onSettled: () => {
                queryClient.invalidateQueries(queryKey)
            },
            onSuccess: () => {
                Toast.show({
                    icon: 'success',
                    content: 'Удален из плейлиста',
                })
            }
        }
    )

    return {
        playlistData: addIdPlaylistToTrack,
        addTrack,
        removeTrack,
        isError: isErrorAdd || isErrorRemove,
        isSuccess: isSuccessRemove || isSuccessAdd,
        isLoading: isLoadingRemove || isLoadingAdd
    }
};