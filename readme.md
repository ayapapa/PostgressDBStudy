# はじめに
C#で, EntityFramworkを使って、Posgres Databaseを操作してみる（工事中）

# ビルド準備
## Postgresをインストール
　see [The PostgreSQL Global Development Group](https://www.postgresql.org/)
 
## NugeパッケージャでEntityFramefork6.npgsqlをインストール
1. プロジェクトファイルをVisualStudioで開く
2. 「ツールメニュー」→「NuGetパッケージマネージャー」→「ソリューションのNuGetパッケージの管理」を開く
3. EntityFramefork6.npgsqlを検索しインストールする


## Npgsqlをアップデートする
EntityFramefork6.npgsqlのインストールで本パッケージ(3.1.0)もインストールされるが、アップデートしないと、エラーが発生し利用できない。
というわけで、6.2.5(2017/10/9時点で最新版）に更新。

## EntityFrameforkをアップデートする
EntityFramefork6.npgsqlのインストールで本パッケージ(6.1.0)もインストールされるが、最新版が出ていたので、6.1.3（2017/10/9時点で最新版）に更新。

# 以下まだ未整理なメモ
PostgresDB接続文字列：
@"Server=localhost;Port=5432;User Id=test; Password=testtest;Database=testDb"

EntityFramwork + Npgsql: 問題あり！！解決策あり
https://stackoverflow.com/questions/40600835/entity-framework-6-npgsql-connection-string-error
Npgsqlを3.2.5に更新
EntityFrameworも祭祀版（6.1.3）に更新

EntityFramework利用：
https://qiita.com/keidrumfreak/items/48df731391174f99b828
日付ー文字列変換
https://www.shift-the-oracle.com/sql/functions/to_date.html
DATE型は比較できる
http://www.earthlink.co.jp/info/1151/


#### Npgsql doesn't support schema creation, so you have to create db manually. Then to avoid this error add this statement somewhere in your code (in your case it might be on the beginning of Main() function):
https://stackoverflow.com/questions/12545228/npgsql-and-entity-framework-code-first-setup-problems

スキーマ名を指定しよう！

IDはシリアルにして、自動付与させよう！　 （[Index(IsUnique = true)]）識別子指定（[Index(IsUnique = true)]）して識別したいものは設定しよう。
https://stackoverflow.com/questions/38803122/postgres-error-null-value-in-column-id-during-insert-operation

# Postgresの場合は、DB、スキーマ、Key、Uniqueなどは、作ってあげないといけない
スキーマ生成スクリプト

```
-- Table: "MySchema"."EncodingInfo"

-- DROP TABLE "MySchema"."EncodingInfo";

CREATE TABLE "MySchema"."EncodingInfo"
(
    name text COLLATE pg_catalog."default" NOT NULL,
    display_name text COLLATE pg_catalog."default" NOT NULL,
    code_page integer NOT NULL,
    id integer NOT NULL DEFAULT nextval('"MySchema"."EncodingInfo_id_seq"'::regclass),
    CONSTRAINT "EncodingInfo_pkey" PRIMARY KEY (id),
    CONSTRAINT code_page_unique UNIQUE (code_page)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE "MySchema"."EncodingInfo"
    OWNER to test;
```
