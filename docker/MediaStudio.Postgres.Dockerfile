FROM postgres:13 AS final
EXPOSE 5432

COPY docker-entrypoint-unitdb.d/init-user-db.sh /docker-entrypoint-unitdb.d/

RUN mkdir /temp
RUN rm -rf /etc/localtime
RUN ln -s /usr/share/zoneinfo/Europe/Moscow /etc/localtime