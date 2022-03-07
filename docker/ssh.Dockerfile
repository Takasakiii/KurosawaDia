FROM ubuntu:20.04

ARG password
ARG username

RUN apt update
RUN apt install openssh-server sudo -y

RUN useradd -rm -d /home/${username} -s /bin/bash -g root -G sudo -u 1000 ${username}

RUN echo ${username}:${password} | chpasswd

RUN service ssh start

EXPOSE 22

CMD ["/usr/sbin/sshd","-D"]
