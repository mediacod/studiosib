import React, {useContext} from 'react';
import {useMutation, useQuery, useQueryClient} from "react-query";
import {AlbumAPI, USER_FAVOURITES_TRACK, USER_PLAYLIST_API} from "../api/api";
import AuthContext from "../context/authContext";

export const useUserPlaylistQuery = () => {

    const {user}:any = useContext(AuthContext)
    const queryClient = useQueryClient()
    const queryKey = ['userPlaylist']

    const {data} = useQuery(queryKey, ()=>USER_PLAYLIST_API.GET(), {enabled: !!user?.idUser})

    const createPlaylistMutation = useMutation(
        (data) => USER_PLAYLIST_API.CREATE(data),
        {
            // Когда вызывается mutate:
            onMutate: async (newData: any) => {
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
            },
            // Always refetch after error or success:
            onSettled: () => {
                queryClient.invalidateQueries(queryKey)
            },
        }
    )

    const removePlaylistMutation = useMutation(
        (id) => USER_PLAYLIST_API.CREATE(id),
        {
            // Когда вызывается mutate:
            onMutate: async (id: any) => {
                // Отмените все исходящие повторные выборки
                await queryClient.cancelQueries(queryKey)

                // Сохраняем предыдущие значения
                const previous: any = queryClient.getQueryData(queryKey)

                //Оптимистично обновите до нового значения
                if (previous) {
                    queryClient.setQueryData(queryKey, {
                        ...previous,
                        data: [...previous?.data?.filter((p: any) => p.idTrack !== id)]
                    })
                }

                return { previous }
            },
            // If the mutation fails, use the context returned from onMutate to roll back
            onError: (err, variables, context) => {
                if (context?.previous) {
                    queryClient.setQueryData(queryKey, context.previous)
                }
            },
            // Always refetch after error or success:
            onSettled: () => {
                queryClient.invalidateQueries(queryKey)
            },
        }
    )

    return {data: data?.data, createPlaylistMutation, removePlaylistMutation}
};