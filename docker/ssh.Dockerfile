FROM ubuntu:20.04

RUN apt update

RUN apt install openssh-server sudo -y

RUN useradd -rm -d /home/main -s /bin/bash -g root -G sudo -u 1000 main

RUN echo 'main:bolinhodecake' | chpasswd

RUN service ssh start

EXPOSE 22

CMD ["/usr/sbin/sshd","-D"]
