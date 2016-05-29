#!/bin/sh

export LANG=ja_JP.UTF-8

REPOS="$1"
REV="$2"
TXN_NAME="$3"

# ------------------------------
# Slack Setting
# ------------------------------
# 通知するSlackのチェンネルを書く（プライベートグループの時は「#」は不要)
channel="#svn_commit"
# 通知するときのユーザ名
username="takeru"
# 通知するユーザのアイコン
iconemoji=":shell:"
# WebHookURL（環境に合わせて下さい。）
webhookUrl="https://hooks.slack.com/services/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"

# ------------------------------
# SVN Setting
# ------------------------------
# Subversionのリポジトリのベースディレクトリ（環境に合わせて下さい。例：/home/svn/repos）
baseRepository="https://github.com/OkamotoSunTower/KawamuraTeam"
# Subversionのリポジトリ名（環境に合わせて下さい。例：svn-test）
repositoryName="trunk"
# Subversionのリポジトリ
repository=$baseRepository/$repositoryName
# コミットしたAuthorを取得
author=$(/usr/bin/svnlook author -r $REV $repository)
# コミットメッセージを取得
commit_msg=$(/usr/bin/svnlook log -r $REV $repository | tr '\n' '\\' | sed 's/\\/\\n/g')
# コミットサマリーを取得
commit_summary=$(/usr/bin/svnlook changed -r $REV $repository | tr '\n' '\\' | sed 's/\\/\\n/g')
# コミット通知するときのヘッダ
header="New commit:"
# コミット通知するときにボディ（commit_summaryとか不要だったら省く)
value="Revision:$REV\n$commit_msg\n$commit_summary"

# Slackに通知
/usr/bin/curl -X POST --data-urlencode "payload={\"channel\": \"${channel}\", \"username\": \"${username}\", \"text\": \"${header}\", \"attachments\": [{ \"fallback\": \"${header}\", \"color\": \"good\", \"fields\": [{\"title\": \"${author}\", \"value\":\"${value}\"  }]  }], \"icon_emoji\": \"${iconemoji}\"}" ${webhookUrl}