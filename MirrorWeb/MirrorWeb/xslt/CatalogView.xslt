<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="toc/tocdiv">
    <li>
      <p>
        <img src="../images/fjzhankai.gif"></img>
        <a target="_blank">
          <xsl:attribute name="href">
            /AdminknReader/Default.aspx?page=<xsl:value-of select="@pagenum"/>#hrefparams#
          </xsl:attribute>
          <xsl:value-of select="./title"/>
        </a>
        <span style="display:none;">
          <xsl:value-of select="@pagenum"/>
        </span>
      </p>
      <xsl:if test="tocdiv[node()!='']" >
        <ul>
          <xsl:for-each select="tocdiv">
            <li>
              <p>
                <img src="../images/fjzhankai.gif"></img>
                <a target="_blank">
                  <xsl:attribute name="href">
                    /AdminknReader/Default.aspx?page=<xsl:value-of select="@pagenum"/>#hrefparams#
                  </xsl:attribute>
                  <xsl:value-of select="./title"/>
                </a>
                <span style="display:none;">
                  <xsl:value-of select="@pagenum"/>
                </span>
              </p>
              <xsl:if test="tocdiv[node()!='']" >
                <ul>
                  <xsl:for-each select="tocdiv">
                    <li>
                      <p>
                        <!--<img src="../images/fjzhankai.gif"></img>-->
                        <a target="_blank">
                          <xsl:attribute name="href">
                            /AdminknReader/Default.aspx?page=<xsl:value-of select="@pagenum"/>#hrefparams#
                          </xsl:attribute>
                          <xsl:value-of select="./title"/>
                        </a>
                        <span style="display:none;">
                          <xsl:value-of select="@pagenum"/>
                        </span>
                      </p>
                      <xsl:if test="tocdiv[node()!='']" >
                        <ul>
                          <xsl:for-each select="tocdiv">
                            <li>
                              <p>
                                <!--<img src="../images/fjzhankai.gif"></img>-->
                                <a target="_blank">
                                  <xsl:attribute name="href">
                                    /AdminknReader/Default.aspx?page=<xsl:value-of select="@pagenum"/>#hrefparams#
                                  </xsl:attribute>
                                  <xsl:value-of select="./title"/>
                                </a>
                                <span style="display:none;">
                                  <xsl:value-of select="@pagenum"/>
                                </span>
                              </p>
                            </li>
                          </xsl:for-each>
                        </ul>
                      </xsl:if>
                    </li>
                  </xsl:for-each>
                </ul>
              </xsl:if>
            </li>
          </xsl:for-each>
        </ul>
      </xsl:if>
    </li>
  </xsl:template>
</xsl:stylesheet>
