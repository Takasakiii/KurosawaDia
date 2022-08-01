FROM rust:1.62.1 as build
COPY . /app
WORKDIR /app
RUN cargo build --release

FROM debian:10.4
RUN apt update
RUN apt install -y curl openssl libssl-dev
COPY --from=build /app/target/release/kurosawa_dia /app/kurosawa_dia
WORKDIR /app
RUN chmod +x kurosawa_dia
ENTRYPOINT [ "./kurosawa_dia" ]
