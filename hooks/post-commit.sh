#!/bin/sh

export LANG=ja_JP.UTF-8

REPOS="$1"
REV="$2"
TXN_NAME="$3"

# ------------------------------
# Slack Setting
# ------------------------------
# �ʒm����Slack�̃`�F���l���������i�v���C�x�[�g�O���[�v�̎��́u#�v�͕s�v)
channel="#svn_commit"
# �ʒm����Ƃ��̃��[�U��
username="takeru"
# �ʒm���郆�[�U�̃A�C�R��
iconemoji=":shell:"
# WebHookURL�i���ɍ��킹�ĉ������B�j
webhookUrl="https://hooks.slack.com/services/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"

# ------------------------------
# SVN Setting
# ------------------------------
# Subversion�̃��|�W�g���̃x�[�X�f�B���N�g���i���ɍ��킹�ĉ������B��F/home/svn/repos�j
baseRepository="https://github.com/OkamotoSunTower/KawamuraTeam"
# Subversion�̃��|�W�g�����i���ɍ��킹�ĉ������B��Fsvn-test�j
repositoryName="trunk"
# Subversion�̃��|�W�g��
repository=$baseRepository/$repositoryName
# �R�~�b�g����Author���擾
author=$(/usr/bin/svnlook author -r $REV $repository)
# �R�~�b�g���b�Z�[�W���擾
commit_msg=$(/usr/bin/svnlook log -r $REV $repository | tr '\n' '\\' | sed 's/\\/\\n/g')
# �R�~�b�g�T�}���[���擾
commit_summary=$(/usr/bin/svnlook changed -r $REV $repository | tr '\n' '\\' | sed 's/\\/\\n/g')
# �R�~�b�g�ʒm����Ƃ��̃w�b�_
header="New commit:"
# �R�~�b�g�ʒm����Ƃ��Ƀ{�f�B�icommit_summary�Ƃ��s�v��������Ȃ�)
value="Revision:$REV\n$commit_msg\n$commit_summary"

# Slack�ɒʒm
/usr/bin/curl -X POST --data-urlencode "payload={\"channel\": \"${channel}\", \"username\": \"${username}\", \"text\": \"${header}\", \"attachments\": [{ \"fallback\": \"${header}\", \"color\": \"good\", \"fields\": [{\"title\": \"${author}\", \"value\":\"${value}\"  }]  }], \"icon_emoji\": \"${iconemoji}\"}" ${webhookUrl}